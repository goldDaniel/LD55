using UnityEngine;
using UnityEngine.Profiling;

public class Minion : RegisteredEnabledBehaviour<Minion>
{
	private Rigidbody2D _body;
	private SpriteRenderer _sr;
	private GameObject _player;

	private float targetTimer = 1.5f;
	private Villager _currentTarget = null;

	private bool _canTargetEnemy = false;

	[SerializeField]
	private AudioClip[] _spawnSounds;

	void Start()
	{
		_body = GetComponent<Rigidbody2D>();
		_player = PlayerController.instance.gameObject;
		_sr = GetComponent<SpriteRenderer>();
		CreateLightning();
		PlaySpawnSound();
	}

	void CreateLightning()
    {
		Vector2 offset = new Vector2(Random.Range(0f, 5f), 10);
		Vector2 end = transform.position;
		Vector2 start = end + offset;
		int nodeCount = 80;
		float branchProbability = 0.8f;
		Vector2 variance = new Vector2(2f, 1f);
		LightningRenderer.instance.CreateLightning(start, end, nodeCount, branchProbability, variance);
    }

	void Update()
	{
		_sr.flipX = _body.velocity.x < 0;

		if (_currentTarget == null)
		{
			targetTimer = 1.5f;
		}
		else 
		{
			targetTimer -= Time.deltaTime;
			if(targetTimer <= 0)
			{
				_currentTarget.isTargeted = false;
				_currentTarget = null;
				_canTargetEnemy = false;
			}
		}

	}

	void FixedUpdate()
	{
		_body.AddForce(Separation());

		if(_currentTarget == null)
		{
			FollowPlayer();
			
		}
		
		if(_canTargetEnemy)
		{
			AttackVillager();
		}

		_body.velocity = Vector2.ClampMagnitude(_body.velocity, 10f);
	}

	void FollowPlayer()
	{
		Vector2 toPlayer = _player.transform.position - this.transform.position;

		if(toPlayer.magnitude > 3)
		{
			_body.AddForce(toPlayer.normalized * 5);
		}
		else
		{
			_canTargetEnemy = true;
			_body.velocity /= 1.25f;
		}
	}

	void AttackVillager()
	{
		float attackDistance = 10f;
		float disengageDistance = 15f;

		if (_currentTarget != null)
		{
			Vector2 diff = _currentTarget.transform.position - _player.transform.position;

			if (diff.sqrMagnitude >= disengageDistance * disengageDistance)
			{
				_currentTarget.isTargeted = false;
				_currentTarget = null;
				_canTargetEnemy = false;
			}
			else
			{
				_body.AddForce(diff * 2f);
			}
			return;
		}

		foreach (var villager in Villager.instances)
		{
			Vector2 diff = villager.transform.position - _player.transform.position;
			if (!villager.isTargeted)
			{
				if (diff.sqrMagnitude < attackDistance * attackDistance)
				{
					villager.isTargeted = true;
					_currentTarget = villager;
					_body.AddForce(diff * 2f);
					return;
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.TryGetComponent(out Villager villager))
		{
			_currentTarget = null;
			_canTargetEnemy = false;
			villager.Die();
			_body.velocity = Vector2.zero;
		}
	}

	Vector2 Separation()
	{
		Vector2 result = new Vector2();

		var allMinions = Minion.instances;

		int maxCount = 20;
		int totalCount = 0;

		int count = 0;
		float separationRange = 1f;

		foreach (var other in allMinions)
		{
			if (totalCount >= maxCount) break;
			if (other == this) continue;
			totalCount++;

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

	void PlaySpawnSound()
	{
		int index = Random.Range(0, _spawnSounds.Length);
		float volume = 0.5f;

		AudioSystem.instance.audioSource.PlayOneShot(_spawnSounds[index], volume);
	}
}
