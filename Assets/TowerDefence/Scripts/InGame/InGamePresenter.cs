using System;
using R3;
using TowerDefence.Scripts.InGame.Model;
using TowerDefence.Scripts.View;
using UnityEngine;

namespace TowerDefence.Scripts.Presenter.Implement
{
    public class InGamePresenter : MonoBehaviour, IDisposable
    {
        [SerializeField] private SimpleText simpleText;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private CharacterButton characterButtonPrefab;
        [SerializeField] private Transform characterButtonParent;
        private readonly CompositeDisposable _disposable = new();

        private void Start()
        {
            gameManager.Cost.Subscribe(CostUpdate).AddTo(_disposable);

            foreach (var character in gameManager.characterScriptableList)
            {
                var characterButton = Instantiate(characterButtonPrefab, characterButtonParent);
                
                var onSpawnSubject = character.OnSpawn;
                var sprite = character.sprite;
                var cost = character.cost;

                characterButton.Initialize(onSpawnSubject, sprite, cost, gameManager.Cost);
            }
        }

        private void CostUpdate(float cost)
        {
            // コストを切り捨てして表示
            var costString = Mathf.FloorToInt(cost).ToString();
            simpleText.SetText(costString);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
