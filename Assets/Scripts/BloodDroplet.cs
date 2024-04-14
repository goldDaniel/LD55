using System;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UIElements;

public class BloodDroplet : RegisteredEnabledBehaviour<BloodDroplet>
{
	private Rigidbody2D _body;

	private float collectionTimer = 0.75f;

	void Start()
	{
		_body = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (collectionTimer > 0)
		{
			collectionTimer -= Time.deltaTime;
		}
	}

	void FixedUpdate()
	{
		_body.velocity /= 1.05f;
		if (collectionTimer > 0)
		{
			return;
		}

		if(!AttractTo(PlayerController.instance.transform.position))
		{
			foreach (var minion in Minion.instances)
			{
				if(AttractTo(minion.transform.position))
				{
					break;
				}
			}
		}

		_body.AddForce(Separation());
	}

	Vector2 Separation()
	{
		Vector2 result = new Vector2();

		var allBlood = BloodDroplet.instances;

		int count = 0;
		float separationRange = 0.5f;

		foreach (var other in allBlood)
		{
			if (other == this) continue;

			Vector2 diff = this.transform.position - other.transform.position;
			if (diff.sqrMagnitude > 0 && diff.sqrMagnitude < (separationRange * separationRange))
			{
				result += diff * 2;
				count++;
			}
		}

		if (count > 0) result /= count;

		return result;
	}

	bool AttractTo(Vector3 attractPosition)
	{
		float attractionRange = 3f;
		float dist = Vector3.Distance(attractPosition, transform.position);

		if (dist < attractionRange)
		{
			Vector3 dir = attractPosition - transform.position;
			dir.Normalize();

			float t = dist / attractionRange;
			_body.velocity += new Vector2(dir.x, dir.y) * t * 2f;
			return true;
		}

		return false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (collectionTimer > 0) return;

		if (other.CompareTag("Player"))
		{
			PlayerInventory.instance.bloodCount++;
			Destroy(this.gameObject);
		}
	}
}
