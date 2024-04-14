using UnityEngine;

public class Villager : RegisteredEnabledBehaviour<Villager>
{
	[SerializeField]
	private GameObject[] _deathTexturePrefabs;

	[SerializeField]
	private float _explosionSize = 2f;

	[SerializeField]
	private GameObject _blood;

	[SerializeField]
	private AudioClip[] _deathSounds;

	[System.NonSerialized]
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
		PlayDeathSound();
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

	void PlayDeathSound()
    {
		int index = Random.Range(0, _deathSounds.Length);
		float volume = 0.5f;

		AudioSystem.instance.audioSource.PlayOneShot(_deathSounds[index], volume);
	}
}
