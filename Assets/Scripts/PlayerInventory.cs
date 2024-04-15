using Assets.Scripts;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoSingleton<PlayerInventory>
{
	[SerializeField]
	private SerializableDictionary<CollectableType, TextMeshProUGUI> _textArea;

	[SerializeField]
	private TextMeshProUGUI minionCost;

	public Dictionary<CollectableType, int> inventory = new()
	{
		{CollectableType.Blood, 10 },
		{CollectableType.Flesh, 0 },
		{CollectableType.Soul, 0 },
	};

	void Update()
	{
		foreach (var entry in inventory)
		{
			var key = entry.Key;
			string text = _textArea.dictionary[key].text;
			string desc = text.Substring(0, text.IndexOf(" "));
			_textArea.dictionary[key].text = $"{desc} {inventory[key]}";
		}

		minionCost.text = $"Minion Cost: {PlayerController.instance.MinionCost} Blood";
	}

	public bool HasEnough(CollectableType type, int goal)
	{
		return inventory[type] >= goal;
	}
}
