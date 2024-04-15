using UnityEngine;

public class SpawnZone : MonoBehaviour
{

	[SerializeField]
	GameObject toSpawn;

	[SerializeField]
	float width, height;

	[SerializeField]
	float spawnTimer;

	float _spawnTime;

	private Vector2 spawnBounds => new Vector2(width * transform.parent.localScale.x, height * transform.parent.localScale.y);

	void Start()
	{
		_spawnTime = spawnTimer;
	}


	void Update()
	{
		_spawnTime -= Time.deltaTime;
		if(_spawnTime <= 0)
		{
			Vector2 pos = new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle * spawnBounds;
			var go = Instantiate(toSpawn, pos, Quaternion.identity);

			go.GetComponentInChildren<Rigidbody2D>().velocity = Random.insideUnitCircle * 5f;

			_spawnTime = spawnTimer;
		}
	}

	private void OnDrawGizmos()
	{
		var color = Gizmos.color;
		Gizmos.color = Color.yellow;

		Gizmos.DrawWireCube(transform.position, new Vector3(spawnBounds.x, spawnBounds.y, 0));

		Gizmos.color = color;
	}
}
