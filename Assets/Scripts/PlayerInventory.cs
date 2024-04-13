using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

	[SerializeField]
	private TextMeshProUGUI textArea;

	public int bloodCount;

	public int bloodGoal;


	void Update()
	{
		textArea.text = $"Blood: {bloodCount} / {bloodGoal}";
	}
}
