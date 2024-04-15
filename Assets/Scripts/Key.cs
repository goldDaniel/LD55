using Assets.Scripts;
using TMPro;
using UnityEngine;

public class Key : MonoBehaviour
{
	[SerializeField]
	Door door;

	[SerializeField]
	int cost;

	private bool paid = false;

	TextMeshPro worldSpaceUI;

	void Start()
	{
		worldSpaceUI = GetComponentInChildren<TextMeshPro>();
		worldSpaceUI.text = $"Required Blood: {cost}";
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (paid) return;
		
		if(collision.CompareTag("Player"))
		{
			if(PlayerInventory.instance.HasEnoughBlood(cost))
			{
				door.Unlock();
				PlayerInventory.instance.inventory[CollectableType.Blood] -= cost;
				paid = true;
				worldSpaceUI.text = "Blood price paid";
			}
		}
	}
}