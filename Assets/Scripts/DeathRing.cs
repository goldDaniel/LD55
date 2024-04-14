using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DeathRing : MonoBehaviour
{
	[SerializeField]
	private Sprite[] _frames;
	private int currentIndex = 0;

	[SerializeField, Range(0.01f, 1f)]
	public float updateRate;

	private float _updateTimer;

	private SpriteRenderer _sr;

	void Start()
	{
		_updateTimer = updateRate;
		_sr = GetComponent<SpriteRenderer>();

		_sr.sprite = _frames[currentIndex];
	}

	// Update is called once per frame
	void Update()
	{
		_updateTimer -= Time.deltaTime;

		if(_updateTimer <= 0)
		{
			currentIndex = (currentIndex + 1) % _frames.Length;
			_sr.sprite = _frames[currentIndex];

			_updateTimer += updateRate;
		}

		this.transform.Rotate(new Vector3(0, 0, 1), -120 * Time.deltaTime);
	}
}
