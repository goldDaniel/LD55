using System.Collections;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
	SpriteRenderer _sr;

	void Start()
	{
		_sr = GetComponent<SpriteRenderer>();
		StartCoroutine(FadeOut());
	}

	IEnumerator FadeOut()
	{
		while (_sr.color.a > 0)
		{
			Color c = _sr.color;
			c.a -= Time.deltaTime;
			_sr.color = c;

			yield return null;
		}

		Destroy(this.gameObject);
	}
}
