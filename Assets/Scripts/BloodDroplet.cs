using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BloodDroplet : MonoBehaviour
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

		float attractionRange = 3f;

		Vector3 playerPos = PlayerController.instance.transform.position;
		float dist = Vector3.Distance(playerPos, transform.position);

		if (dist < attractionRange)
		{
			Vector3 dir = playerPos - transform.position;
			dir.Normalize();

			float t = dist / attractionRange;
			_body.velocity += new Vector2(dir.x, dir.y) * t * 2f;
		}
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
