using Assets.Scripts;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoSingleton<PlayerInventory>
{
	[SerializeField]
	private SerializableDictionary<CollectableType, TextMeshProUGUI> _textArea = new SerializableDictionary<CollectableType, TextMeshProUGUI>();

    public Dictionary<CollectableType, int> inventory = new Dictionary<CollectableType, int>();

    private void Start()
    {
		foreach (CollectableType type in System.Enum.GetValues(typeof(CollectableType)))
        {
			int count = 0;
			if (type == CollectableType.Blood) count = 10;

			inventory.Add(type, count);
        }
    }

    void Update()
	{
		foreach (var entry in inventory)
        {
			var key = entry.Key;
			string text = _textArea.dictionary[key].text;
			string desc = text.Substring(0, text.IndexOf(" "));
			_textArea.dictionary[key].text = $"{desc} {inventory[key]}";
        }
	}

	public bool HasEnough(CollectableType type, int goal)
	{
		return inventory[type] >= goal;
	}
}
