using UnityEngine;

public class Minion : RegisteredEnabledBehaviour<Minion>
{
	private Rigidbody2D _body;
	private SpriteRenderer _sr;
	private GameObject _player;

	private GameObject _currentTarget = null;

	void Start()
	{
		_body = GetComponent<Rigidbody2D>();
		_player = PlayerController.instance.gameObject;
		_sr = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		_sr.flipX = _body.velocity.x < 0;
	}

	void FixedUpdate()
	{
		_body.AddForce(Separation());

		AttackVillager();
		FollowPlayer();
	}

	void FollowPlayer()
	{
		Vector2 toPlayer = _player.transform.position - this.transform.position;

		if(toPlayer.magnitude > 2)
		{
			float speed = toPlayer.magnitude * 3f;
			_body.AddForce(toPlayer.normalized * speed);
		}
	}

	void AttackVillager()
	{
		float attackDistance = 5f;
		float disengageDistance = 10f;

		if (_currentTarget != null)
		{
			Vector2 diff = _currentTarget.transform.position - _player.transform.position;


			if (diff.sqrMagnitude >= disengageDistance * disengageDistance)
			{
				_currentTarget = null;
			}
			else
			{
				_body.AddForce(diff * 5f);
				return;
			}
		}

		foreach (var villager in Villager.instances)
		{
			Vector2 diff = villager.transform.position - _player.transform.position;

			if(diff.sqrMagnitude < attackDistance*attackDistance)
			{
				_currentTarget = villager.gameObject;
				_body.AddForce(diff * 5f);
				return;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.TryGetComponent(out Villager villager))
		{
			_currentTarget = null;
			villager.Die();
			FollowPlayer();
		}
	}

	Vector2 Separation()
	{
		Vector2 result = new Vector2();

		var allMinions = Minion.instances;

		int count = 0;
		float separationRange = 1f;

		foreach (var other in allMinions)
		{
			if (other == this) continue;

			Vector2 diff = this.transform.position - other.transform.position;
			if(diff.sqrMagnitude > 0 && diff.sqrMagnitude < (separationRange*separationRange))
			{
				result += diff * 5;
				count++;
			}
		}

		if (count > 0) result /= count;

		return result;
	}
}
