using System.Collections.Generic;
using UnityEngine;

public class RegisteredEnabledBehaviour<T> : MonoBehaviour where T : RegisteredEnabledBehaviour<T>
{
	private static readonly List<T> _instances = new();

	public static IReadOnlyList<T> instances => _instances;

	protected virtual void OnEnable()
	{
		var instance = (T)this;
		if(!_instances.Contains(instance))
		{
			_instances.Add(instance);
		}
	}

	protected virtual void OnDisable()
	{
		_instances.Remove((T)this);
	}
}
