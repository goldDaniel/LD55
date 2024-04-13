using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField, Range(1f, 5f)]
	private float _speed;
	
	void Update()
	{
		Vector3 movement = Vector3.zero;

		movement.x = Input.GetAxis("Horizontal");
		movement.y = Input.GetAxis("Vertical");

		movement.Normalize();

		transform.position += movement * _speed * Time.deltaTime;
	}
}
