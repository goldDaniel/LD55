using UnityEngine;

public class Key : MonoBehaviour
{
	[SerializeField]
	Door door;

	[SerializeField]
	int cost;

	private bool paid = false;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (paid) return;
		
		if(collision.CompareTag("Player"))
		{
			if(PlayerInventory.instance.HasEnoughBlood(cost))
			{
				door.Unlock();
				PlayerInventory.instance.bloodCount -= cost;
				paid = true;
			}
		}
	}
}