using Assets.Scripts;
using TMPro;
using UnityEngine;

public class Key : MonoBehaviour
{
	[System.Serializable]
	private class Payment
    {
		[SerializeField]
		public CollectableType type;
		[SerializeField]
		public int cost;
    }

	[SerializeField]
	Door door;

	[SerializeField]
	Payment[] payment;

	private bool paid = false;

	TextMeshPro worldSpaceUI;

	void Start()
	{
		worldSpaceUI = GetComponentInChildren<TextMeshPro>();
		string text = "Required payment\n";
		foreach (var pay in payment)
        {
			string payName = pay.type.ToString();
			string payText = $"{payName} {pay.cost} \n";
			text += payText;
        }

		worldSpaceUI.text = text;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (paid) return;
		
		if(collision.CompareTag("Player"))
		{
			bool costReached = true;
			foreach (var pay in payment)
			{
				costReached &= PlayerInventory.instance.HasEnough(pay.type, pay.cost);
			}

			if(costReached)
			{
				door.Unlock();
				foreach (var pay in payment)
				{
					PlayerInventory.instance.inventory[pay.type] -= pay.cost;
				}

				paid = true;
				worldSpaceUI.text = "Ritual Completed";
			}
		}
	}
}