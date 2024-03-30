using TMPro;
using UnityEngine;

namespace TowerDefence.Scripts.View
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class SimpleText : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}
