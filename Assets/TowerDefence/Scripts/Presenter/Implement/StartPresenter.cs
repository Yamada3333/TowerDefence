using System;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefence.Scripts.Presenter.Implement
{
    public class StartPresenter : MonoBehaviour, IDisposable
    {
        private const string OutGameSceneName = "OutGameScene";
        
        private readonly CompositeDisposable _disposable = new();
        
        private void Start()
        {
            //startButton.OnClick.Subscribe(SceneChange).AddTo(_disposable);
        }

        private void SceneChange(Unit _)
        {
            SceneManager.LoadScene(OutGameSceneName);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
