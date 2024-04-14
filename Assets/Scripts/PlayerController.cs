using System.Collections;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
	[SerializeField, Range(1f, 5f)]
	private float _speed;

	private Rigidbody2D _body;

	private Vector2 _direction;

	void Awake()
	{
		_body = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		_direction = Vector3.zero;

		_direction.x = Input.GetAxisRaw("Horizontal");
		_direction.y = Input.GetAxisRaw("Vertical");

		_direction.Normalize();
	}

	void FixedUpdate()
	{
		_body.velocity = _direction * _speed;
	}
}
