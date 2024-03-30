using DG.Tweening;
using R3;
using TMPro;
using TowerDefence.Scripts.Presenter.Interface;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerDefence.Scripts.View.Implement
{
    public class SimpleButton : MonoBehaviour, ISimpleButton, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        private readonly Subject<Unit> _onClick = new();
        public Observable<Unit> OnClick => _onClick;
        
        private const float Duration = 0.2f;
        
        [SerializeField] private Image background;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI text;

        private void Awake()
        {
            icon.color = Color.clear;
            text.text = "";
        }
        
        public void SetBackground(Sprite sprite)
        {
            background.sprite = sprite;
        }
        
        public void SetIcon(Sprite sprite)
        {
            icon.sprite = sprite;
            icon.color = Color.white;
        }
        
        public void SetText(string str)
        {
            text.text = str;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _onClick.OnNext(Unit.Default);
        }

        // クリック押した瞬間
        public void OnPointerDown(PointerEventData eventData)
        {
            transform.DOScale(0.8f, Duration).SetEase(Ease.OutCubic);
        }

        // クリック離した瞬間
        public void OnPointerUp(PointerEventData eventData)
        {
            transform.DOScale(1.0f, Duration).SetEase(Ease.OutCubic);
        }
    }
}
