using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;

namespace Assets.Scripts
{
    [Serializable]
    public class CollectableToTextUIDictionary : SerializableDictionary<CollectableType, TextMeshProUGUI> { }
}
