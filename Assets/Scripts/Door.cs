using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	public virtual void Unlock()
	{
		Destroy(this.gameObject);
	}
}
