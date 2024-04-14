using UnityEngine;

public class Key : MonoBehaviour
{
	[SerializeField]
	Door door;

	[SerializeField]
	int cost;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			if(PlayerInventory.instance.HasEnoughBlood(cost))
			{
				door.Unlock();
				PlayerInventory.instance.bloodCount -= cost;
			}
		}
	}
}