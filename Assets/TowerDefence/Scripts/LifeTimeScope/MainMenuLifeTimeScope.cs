using System.Collections.Generic;
using TowerDefence.Scripts.Entity.Scriptable;
using TowerDefence.Scripts.Presenter.Implement;
using TowerDefence.Scripts.Presenter.Interface;
using TowerDefence.Scripts.UseCase.Data;
using TowerDefence.Scripts.UseCase.Implement;
using TowerDefence.Scripts.UseCase.Interface.Presenter;
using TowerDefence.Scripts.View.Implement;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace TowerDefence.Scripts.LifeTimeScope
{
    public class MainMenuLifeTimeScope : LifetimeScope
    {
        [SerializeField] private Transform mainMenuButtonParent;
        
        [SerializeField] private List<MainMenuButtonScriptable> mainMenuButtonScriptableList;
        [SerializeField] private SimpleButton simpleButton;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IMainMenuPresenter, MainMenuPresenter>(Lifetime.Scoped);
            builder.Register<MainManuInitializeUseCase>(Lifetime.Scoped);
            builder.RegisterInstance(mainMenuButtonScriptableList);
            
            // UIをスクリプトで動的に生成する場合は、RegisterFactoryで登録する
            builder.RegisterFactory<MainMenuButtonData, ISimpleButton>(Instantiate);

            builder.RegisterEntryPoint<MainManuInitializeUseCase>();
        }
        
        private ISimpleButton Instantiate(MainMenuButtonData data)
        {
            var simpleButtonObject = Instantiate(simpleButton, mainMenuButtonParent);
            simpleButtonObject.SetIcon(data.Sprite);
            simpleButtonObject.SetText(data.ButtonText);
            return simpleButtonObject;
        }
    }
}
