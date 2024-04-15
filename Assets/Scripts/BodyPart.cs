using System.Collections;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
	SpriteRenderer _sr;

	void Start()
	{
		_sr = GetComponent<SpriteRenderer>();
		GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-360f, 360f);

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
}
