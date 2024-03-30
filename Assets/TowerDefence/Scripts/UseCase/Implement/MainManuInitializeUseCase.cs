using System.Collections.Generic;
using TowerDefence.Scripts.Entity.Scriptable;
using TowerDefence.Scripts.UseCase.Data;
using TowerDefence.Scripts.UseCase.Interface.Presenter;
using VContainer;
using VContainer.Unity;

namespace TowerDefence.Scripts.UseCase.Implement
{
    public class MainManuInitializeUseCase : IStartable
    {
        private readonly IMainMenuPresenter _mainMenuPresenter;
        private readonly List<MainMenuButtonScriptable> _mainMenuButtonScriptableList;
        
        [Inject]
        public MainManuInitializeUseCase(List<MainMenuButtonScriptable> mainMenuButtonScriptableList, IMainMenuPresenter mainMenuPresenter)
        {
            _mainMenuPresenter = mainMenuPresenter;
            _mainMenuButtonScriptableList = mainMenuButtonScriptableList;
        }

        public void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            foreach (var mainMenuButtonScriptable in _mainMenuButtonScriptableList)
            {
                var sprite = mainMenuButtonScriptable.sprite;
                var sceneName = mainMenuButtonScriptable.sceneName;
                var buttonText = mainMenuButtonScriptable.buttonText;
                var mainMenuButtonData = new MainMenuButtonData(sprite, buttonText, sceneName);
                
                _mainMenuPresenter.InstantiateButton(mainMenuButtonData);
            }
        }
    }
}
