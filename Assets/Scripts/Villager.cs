using UnityEngine;

public class Villager : RegisteredEnabledBehaviour<Villager>
{
	[SerializeField]
	private GameObject[] _deathTexturePrefabs;

	[SerializeField]
	private float _explosionSize = 2f;

	[SerializeField]
	private GameObject _blood;

	public bool isTargeted = false;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			Die();
		}
	}

	public void Die()
	{
		isTargeted = false;
		SpawnBloodParticles();
		for (int i = 0; i < _deathTexturePrefabs.Length; i++)
		{
			GameObject bodyPart = Instantiate(_deathTexturePrefabs[i], transform.position, Quaternion.identity);
			bodyPart.GetComponent<Rigidbody2D>().velocity = Random.insideUnitSphere * _explosionSize;
		}
		Destroy(gameObject);
	}

	void SpawnBloodParticles()
	{
		int bloodDroplet = UnityEngine.Random.Range(4, 10);

		for (int i = 0; i < bloodDroplet; ++i)
		{
			GameObject obj = Instantiate(_blood, transform.position, Quaternion.identity);
			obj.GetComponent<Rigidbody2D>().velocity = Random.insideUnitSphere * Random.Range(5, 12);
		}
	}
}
