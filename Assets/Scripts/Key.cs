using Assets.Scripts;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
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

	[SerializeField]
	AudioClip ritualClip;

	private bool paid = false;

	private bool canUnlock = false;
	TextMeshPro pressToUnlock;
	TextMeshPro worldSpaceCost;

	void Start()
	{
		worldSpaceCost = transform.Find("WorldSpaceText").GetComponent<TextMeshPro>();
		pressToUnlock = transform.Find("InputPrompt").GetComponent<TextMeshPro>();
		string text = "Required payment\n";
		foreach (var pay in payment)
        {
			string payName = pay.type.ToString();
			string payText = $"{payName} {pay.cost} \n";
			text += payText;
        }

		worldSpaceCost.text = text;
	}

	void Update()
	{
		pressToUnlock.gameObject.SetActive(false);
		if (canUnlock && !paid)
		{
			bool costReached = true;
			foreach (var pay in payment)
			{
				costReached &= PlayerInventory.instance.HasEnough(pay.type, pay.cost);
			}

			if (costReached)
			{
				pressToUnlock.gameObject.SetActive(true);
				if (Input.GetKeyDown(KeyCode.Space))
				{
					AudioSystem.instance.audioSource.PlayOneShot(ritualClip, 0.5f);
					door.Unlock();
					foreach (var pay in payment)
					{
						PlayerInventory.instance.inventory[pay.type] -= pay.cost;
					}

					paid = true;
					worldSpaceCost.text = "Ritual Completed";
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			canUnlock = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			canUnlock = false;
		}
	}
}