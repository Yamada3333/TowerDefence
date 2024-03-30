using System;
using R3;
using TowerDefence.Scripts.Presenter.Interface;
using TowerDefence.Scripts.UseCase.Data;
using TowerDefence.Scripts.UseCase.Interface.Presenter;
using UnityEngine.SceneManagement;
using VContainer;

namespace TowerDefence.Scripts.Presenter.Implement
{
    public class MainMenuPresenter : IMainMenuPresenter
    {
        private readonly Func<MainMenuButtonData, ISimpleButton> _unit;
        
        [Inject]
        public MainMenuPresenter(Func<MainMenuButtonData, ISimpleButton> unit)
        {
            _unit = unit;
        }
        
        public void InstantiateButton(MainMenuButtonData mainMenuButtonData)
        {
            var button = _unit.Invoke(mainMenuButtonData);
            button.OnClick.Subscribe(_ => SceneChange(mainMenuButtonData.SceneName));
        }
        
        private void SceneChange(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
