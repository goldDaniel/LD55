using System.Collections;
using UnityEngine;

public class AudioSystem : MonoSingleton<AudioSystem>
{
	[SerializeField]
	public AudioClip intro, loop;

	private AudioSource _audioSource;
	public AudioSource audioSource => _audioSource;


	private AudioSource _introAudioSource;
	private AudioSource _loopAudioSource;


	void Awake()
	{
		_audioSource = gameObject.AddComponent<AudioSource>();

		_introAudioSource = gameObject.AddComponent<AudioSource>();
		_loopAudioSource = gameObject.AddComponent<AudioSource>();

		StartCoroutine(PlayAudio());
	}

	private IEnumerator PlayAudio()
	{
		_introAudioSource.clip = intro;
		_introAudioSource.loop = false;
		_introAudioSource.Play();

		_loopAudioSource.clip = loop;
		_loopAudioSource.loop = true;
		loop.LoadAudioData();

		while (_introAudioSource.isPlaying)
		{
			yield return null;
		}

		_loopAudioSource.Play();
	}
}
