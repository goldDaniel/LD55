using System.Linq;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
	private static T _instance;

	public static T instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = (T)Resources.FindObjectsOfTypeAll(typeof(T)).FirstOrDefault();
				_instance ??= new GameObject(typeof(MonoSingleton<T>).ToString(), typeof(T)).GetComponent<T>();
				if(Application.isPlaying)
				{
					DontDestroyOnLoad(_instance);
				}
			}

			return _instance;
		}
	}
}
