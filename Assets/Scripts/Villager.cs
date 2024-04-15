using Assets.Scripts;
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

	void FixedUpdate()
	{
		Vector2 playerPos = PlayerController.instance.transform.position;
		if(Vector2.Distance(playerPos, this.transform.position) < 10f)
		{
			Vector2 thisPos = this.transform.position;
			Vector2 dir =  (thisPos - playerPos).normalized;

			_body.AddForce(dir * 2f);
		}

		_body.AddForce(Random.insideUnitCircle * Random.Range(1f, 2f));

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

		for(int i = 0; i < amount; ++i)
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
}
