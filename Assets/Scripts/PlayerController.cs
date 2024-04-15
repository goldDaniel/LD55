using Assets.Scripts;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
	[SerializeField, Range(5f, 15f)]
	private float _speed;

	[SerializeField, Range(1, 100)]
	private int _minionCost;

	[SerializeField]
	Minion minionPrefab;

	private SpriteRenderer _sr;
	private Rigidbody2D _body;

	private Vector2 _direction;

	void Awake()
	{
		_body = GetComponent<Rigidbody2D>();
		_sr = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		_direction = Vector3.zero;

		_direction.x = Input.GetAxisRaw("Horizontal");
		_direction.y = Input.GetAxisRaw("Vertical");

		_direction.Normalize();



		if (Input.GetKeyDown(KeyCode.Q))
		{
			if (PlayerInventory.instance.inventory[CollectableType.Blood] >= _minionCost)
			{
				PlayerInventory.instance.inventory[CollectableType.Blood] -= _minionCost;
				SpawnMinion();
			}
		}

		_sr.flipX = _body.velocity.x < 0;
	}

	void FixedUpdate()
	{
		_body.velocity = _direction * _speed;
	}

	void SpawnMinion()
	{
		var spawnPos = new Vector2(this.transform.position.x, this.transform.position.y) + Random.insideUnitCircle * Random.Range(1.5f, 2.5f);
		Instantiate(minionPrefab, spawnPos, Quaternion.identity);
	}
}
