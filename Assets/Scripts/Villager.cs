using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class Villager : RegisteredEnabledBehaviour<Villager>
{
	[System.Serializable]
	private struct DropData
	{
		[SerializeField]
		public Vector2Int amountRange;
		[SerializeField]
		public float probability;
	}

	[SerializeField]
	private GameObject[] _deathTexturePrefabs;

	[SerializeField]
	private float _explosionSize = 2f;

	[SerializeField]
	private SerializableDictionary<GameObject, DropData> _drops = new SerializableDictionary<GameObject, DropData>();

	[SerializeField]
	private AudioClip[] _deathSounds;

	[System.NonSerialized]
	public bool isTargeted = false;

	private Rigidbody2D _body;

	void Start()
	{
		_body = GetComponent<Rigidbody2D>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			Die();
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		List<ContactPoint2D> contacts = new();
		collision.GetContacts(contacts);
		Vector2 normal = new();
		foreach (var contact in contacts)
		{
			normal += contact.normal;
		}
		normal.Normalize();

		_body.velocity = -_body.velocity;
		_body.position += normal * this.transform.localScale.x / 2f;
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		OnCollisionEnter2D(collision);
	}

	void FixedUpdate()
	{
		Vector2 playerPos = PlayerController.instance.transform.position;
		if (Vector2.Distance(playerPos, this.transform.position) < 12f)
		{
			Vector2 thisPos = this.transform.position;
			Vector2 dir = (thisPos - playerPos).normalized;

			_body.AddForce(dir * 2f);
		}

		if (Random.value > 0.25)
		{
			_body.AddForce(Random.insideUnitCircle * Random.Range(5f, 12f));
		}


		_body.velocity = Vector2.ClampMagnitude(_body.velocity, 6f);
	}

	public void Die()
	{
		isTargeted = false;
		foreach (var entry in _drops.dictionary)
		{
			GameObject prefab = entry.Key;
			DropData data = (DropData)entry.Value;
			int amount = Random.Range(data.amountRange.x, data.amountRange.y);
			Spawn(prefab, amount, data.probability);
		}
		PlayDeathSound();
		for (int i = 0; i < _deathTexturePrefabs.Length; i++)
		{
			GameObject bodyPart = Instantiate(_deathTexturePrefabs[i], transform.position, Quaternion.identity);
			bodyPart.GetComponent<Rigidbody2D>().velocity = Random.insideUnitSphere * _explosionSize;
		}
		Destroy(gameObject);
	}

	void Spawn(GameObject prefab, int amount, float probbability)
	{
		if (Random.value > probbability) return;

		for (int i = 0; i < amount; ++i)
		{
			GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
			obj.GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle * Random.Range(5, 12);
		}
	}

	void PlayDeathSound()
	{
		int index = Random.Range(0, _deathSounds.Length);
		float volume = 0.5f;

		AudioSystem.instance.audioSource.PlayOneShot(_deathSounds[index], volume);
	}

	Vector2 Separation()
	{
		Vector2 result = new Vector2();

		var allMinions = Villager.instances;

		int maxCount = 30;
		int totalCount = 0;

		int count = 0;
		float separationRange = 8f;

		foreach (var other in allMinions)
		{
			if (totalCount >= maxCount) break;
			if (other == this) continue;
			totalCount++;

			Vector2 diff = this.transform.position - other.transform.position;
			if (diff.sqrMagnitude > 0 && diff.sqrMagnitude < (separationRange * separationRange))
			{
				result += diff.normalized * 8f;
				count++;
			}
		}

		if (count > 0) result /= count;

		return result;
	}
}
