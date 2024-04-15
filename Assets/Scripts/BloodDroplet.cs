using UnityEngine;

public class BloodDroplet : RegisteredEnabledBehaviour<BloodDroplet>
{
	[SerializeField]
	private AudioClip[] pickEffects;

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

		if(!AttractTo(PlayerController.instance.transform.position, 6f))
		{
			Vector2 dir = PlayerController.instance.transform.position - transform.position;
			_body.velocity = dir.normalized * 0.25f;
		}

		var force = Random.insideUnitCircle * Random.Range(0.5f, 1.5f);
		_body.AddForce(force);
	}

	bool AttractTo(Vector3 attractPosition, float attractionRange)
	{
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
			PlayPickupSound();
			Destroy(this.gameObject);
		}
	}

	void PlayPickupSound()
	{
		int index = Random.Range(0, pickEffects.Length);
		float volume = 0.5f;
		if(index == 4)
		{
			volume = 0.2f;
		}

		AudioSystem.instance.audioSource.PlayOneShot(pickEffects[index], volume);
	}
}
