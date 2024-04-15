using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
	private GameObject _player;
	private Rigidbody2D _body;

	[SerializeField]
	private GameObject _heart;

	[SerializeField]
	private float spawnTime = 1.5f;

	private float passedTime = 0f;

    void Start()
    {
		_player = PlayerController.instance.gameObject;
		_body = GetComponent<Rigidbody2D>();
	}

	void Update()
    {
		FollowPlayer();
		SpawnHearts();

		passedTime += Time.deltaTime;
    }

	void FollowPlayer()
	{
		Vector2 toPlayer = _player.transform.position - this.transform.position;

		if (toPlayer.magnitude > 3)
		{
			_body.AddForce(toPlayer.normalized * 1.5f);
		}
		else
		{
			_body.velocity /= 1.05f;
		}
	}

	void SpawnHearts()
    {
		while (CanSpawn())
		{
			GameObject heart = Instantiate(_heart, transform.position, Quaternion.identity);
		}
	}

	bool CanSpawn()
    {
		bool result = spawnTime <= passedTime;

		if (result)
		{
			passedTime -= spawnTime;
		}

		return result;
    }
}
