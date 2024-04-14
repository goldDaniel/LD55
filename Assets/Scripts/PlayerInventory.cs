using TMPro;
using UnityEngine;

public class PlayerInventory : MonoSingleton<PlayerInventory>
{
	[SerializeField]
	private TextMeshProUGUI textArea;

	public int bloodCount;

	void Update()
	{
		textArea.text = $"Blood Count: {bloodCount}";
	}

	public bool HasEnoughBlood(int goal)
	{
		return bloodCount >= goal;
	}
}
