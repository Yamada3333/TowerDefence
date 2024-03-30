using R3;
using UnityEngine;

namespace TowerDefence.Scripts.Presenter.Interface
{
    public interface ISimpleButton
    {
        Observable<Unit> OnClick { get; }
        void SetBackground(Sprite sprite);
        void SetIcon(Sprite sprite);
        void SetText(string str);
    }
}
