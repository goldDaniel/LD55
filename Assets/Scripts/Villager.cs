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
