using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
	private SpriteRenderer _sr;
	private Rigidbody2D _rb;

	[SerializeField]
	private float _speed = 1f;
	[SerializeField]
	private float flutterChance = 0.3f;
	[SerializeField]
	private float flutterForce = 5f;

	void Start()
	{
        _sr = GetComponent<SpriteRenderer>();
		_rb = GetComponent<Rigidbody2D>();
		_rb.velocity = _speed * Vector2.up;
		StartCoroutine(FadeOut());
	}

	IEnumerator FadeOut()
	{
		while (_sr.color.a > 0)
		{
			Color c = _sr.color;
			c.a -= Time.deltaTime * 0.10f;
			_sr.color = c;

			yield return null;
		}

		Destroy(this.gameObject);
	}

	void Update()
	{
		float chance = Random.value;
		if(chance <= flutterChance)
        {
			int dir = Random.value >= 0.5 ? 1 : -1;
			_rb.AddForce(flutterForce * new Vector2(dir, 0));	
        }
	}
}
