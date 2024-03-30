using R3;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerDefence.Scripts.View
{
    [RequireComponent(typeof(EventTrigger))]
    public class CharacterButton : MonoBehaviour, IPointerClickHandler
    {
        private ISubject<Unit> _observer;
        [SerializeField] private Image background;
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI costText;

        public void Initialize(ISubject<Unit> observer, Sprite sprite, int cost, ReadOnlyReactiveProperty<float> costProperty)
        {
            _observer = observer;
            image.sprite = sprite;
            costText.text = cost.ToString();
            costProperty.Subscribe(x => ChangeColor(x >= cost)).AddTo(gameObject);
        }

        private void ChangeColor(bool isActive)
        {
            // コストが足りない場合はグレーアウト
            var color = isActive ? Color.white : Color.gray;
            background.color = color;
            image.color = color;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            _observer?.OnNext(Unit.Default);
        }
    }
}
