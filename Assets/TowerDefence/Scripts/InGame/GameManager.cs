using System;
using System.Collections.Generic;
using R3;
using R3.Triggers;
using TowerDefence.Scripts.InGame.Model.Scriptable;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TowerDefence.Scripts.InGame.Model
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Character companionSpawnerPrefab;
        [SerializeField] private Character enemySpawnerPrefab;
        [SerializeField] private Character characterPrefab;
        [SerializeField] private StageScriptable stageScriptable;
        public List<CharacterScriptable> characterScriptableList;
        
        public ReactiveProperty<float> Cost { get; } = new();

        private static readonly Vector3 CompanionPosition = new(5, 0, 0);
        private static readonly Vector3 EnemyPosition = new(-5, 0, 0);
        
        private Character _companionSpawner;
        private Character _enemySpawner;
        private int _spawnIndex;
        private float _spawnTime;
        private readonly Subject<float> _gameTimer = new();
        private readonly Func<CharacterScriptable, Character> _unit;
        
        private void Start()
        {
            SpawnCompanionSpawner();
            SpawnEnemySpawner();

            foreach (var scriptable in characterScriptableList)
            {
                scriptable.OnSpawn.Subscribe(_ => SpawnCompanion(scriptable)).AddTo(gameObject);
            }
        }

        private void Update()
        {
            var time = Time.deltaTime;
            _gameTimer.OnNext(time);
            SpawnUpdate(time);
            Cost.Value += time;
        }
        
        private void SpawnUpdate(float time)
        {
            _spawnTime += time;

            // 未出現のキャラクターがいるかチェック
            if (_spawnIndex >= stageScriptable.characterList.Count) return;
            // スポーン時間になったらキャラクターを生成
            if (_spawnTime < stageScriptable.spawnTimeList[_spawnIndex]) return;
            
            SpawnEnemy();
            _spawnIndex++;
            _spawnTime = 0;
        }
        
        private void SpawnCompanion(CharacterScriptable spawnData)
        {
            var layer = _companionSpawner.gameObject.layer;
            var spawnCharacter = Instantiate(characterPrefab, CompanionPosition, Quaternion.identity);
            spawnCharacter.SetCharacter(spawnData, layer, EnemyPosition, _gameTimer);
            AudioManager.Instance.OnSpawn.OnNext(Unit.Default);
        }

        private void SpawnEnemy()
        {
            var random = Random.Range(0, stageScriptable.characterList.Count);
            var spawnData = stageScriptable.characterList[random];
            var layer = _enemySpawner.gameObject.layer;
            var spawnCharacter = Instantiate(characterPrefab, EnemyPosition, Quaternion.identity);
            spawnCharacter.SetCharacter(spawnData, layer, CompanionPosition, _gameTimer);
            AudioManager.Instance.OnSpawn.OnNext(Unit.Default);
        }

        private void SpawnCompanionSpawner()
        {
            _companionSpawner = Instantiate(companionSpawnerPrefab, CompanionPosition, Quaternion.identity);
            _companionSpawner.SetHealth(1000);
            _companionSpawner.OnDestroyAsObservable().Subscribe(GameOver).AddTo(_companionSpawner.gameObject);
        }
        
        private void SpawnEnemySpawner()
        {
            _enemySpawner = Instantiate(enemySpawnerPrefab, EnemyPosition, Quaternion.identity);
            _enemySpawner.SetHealth(1000);
            _enemySpawner.OnDestroyAsObservable().Subscribe(GameClear).AddTo(_enemySpawner.gameObject);
        }

        private void GameOver(Unit _)
        {
            Debug.Log("Game Over");
        }
        
        private void GameClear(Unit _)
        {
            Debug.Log("Game Clear");
        }
    }
}
