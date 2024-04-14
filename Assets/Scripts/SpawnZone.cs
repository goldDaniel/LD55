using UnityEngine;

public class SpawnZone : MonoBehaviour
{

	[SerializeField]
	GameObject toSpawn;

	[SerializeField, Min(1)]
	float width, height;

	[SerializeField]
	float spawnTimer;

	float _spawnTime;

	void Start()
	{
		_spawnTime = spawnTimer;
	}


	void Update()
	{
		_spawnTime -= Time.deltaTime;
		if(_spawnTime <= 0)
		{
			Vector2 pos = new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle * new Vector2(width, height);
			var go = Instantiate(toSpawn, pos, Quaternion.identity);

			go.GetComponentInChildren<Rigidbody2D>().velocity = Random.insideUnitCircle * 2f;

			_spawnTime = spawnTimer;
		}
	}

	private void OnDrawGizmos()
	{
		var color = Gizmos.color;
		Gizmos.color = Color.yellow;

		Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));

		Gizmos.color = color;
	}
}
