using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField]
	private GameObject toFollow;

	void Update()
	{
		float z = this.transform.position.z;

		Vector3 newPos = toFollow.transform.position;
		newPos.z = z;

		this.transform.position = newPos;
	}
}
