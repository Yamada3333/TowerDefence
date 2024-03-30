using System.Collections.Generic;
using DG.Tweening;
using R3;
using TowerDefence.Scripts.InGame.Model.Scriptable;
using UnityEngine;

namespace TowerDefence.Scripts.InGame.Model
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        private CharacterScriptable _scriptable;
        private Transform _transform;

        private float _health;
        private float _attackTimer;
        private Vector3 _targetPosition;
        public Character currentTarget;
        private readonly List<Character> _targets = new();

        public void SetHealth(float health)
        {
            _health = health;
        }

        public void SetCharacter(CharacterScriptable scriptable, int layer, Vector3 targetPosition, Observable<float> gameTimer)
        {
            _scriptable = scriptable;

            spriteRenderer.sprite = _scriptable.sprite;
            var collider2d = GetComponent<CircleCollider2D>();
            _transform = transform;

            // レイヤーを設定
            gameObject.layer = layer;
            // 子供のオブジェクトも含めレイヤーを変更する
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.layer = layer;
            }

            collider2d.radius = _scriptable.size;
            SetHealth(_scriptable.health);
            _targetPosition = targetPosition;

            gameTimer.Subscribe(CharacterUpdate).AddTo(gameObject);
        }

        private void CharacterUpdate(float time)
        {
            AttackUpdate(time);
            MoveUpdate(time);
        }

        private void AttackUpdate(float time)
        {
            _attackTimer += time;
            // ターゲットがいない場合は攻撃しない
            if (currentTarget == null) return;
            // 前回の攻撃からの時間が攻撃速度より短い場合は攻撃しない
            if (_attackTimer < _scriptable.attackSpeed) return;
            AttackAnimation();
            AudioManager.Instance.OnHit.OnNext(Unit.Default);
            currentTarget.TakeDamage(_scriptable.attackPower);
            // 攻撃したらタイマーをリセット
            _attackTimer = 0;
        }

        private void MoveUpdate(float time)
        {
            // ターゲットがいる場合は移動しない
            if (currentTarget != null) return;
            // 目的地に向かって移動
            var speed = _scriptable.moveSpeed * time;
            var currentPosition = _transform.position;
            _transform.position = Vector3.MoveTowards(currentPosition, _targetPosition, speed);
        }

        private void TakeDamage(float damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                AudioManager.Instance.OnDeath.OnNext(Unit.Default);
                Destroy(gameObject);
            }
        }

        private void AttackAnimation()
        {
            // 攻撃アニメーション
            //unitの方向にDotWeenで移動して0.1秒後に元の位置に戻る
            const float time = 0.1f;
            var spriteTransform = spriteRenderer.transform;
            var currentX = transform.position.x;
            var targetX = currentTarget.transform.position.x;
            // normalizedで方向は維持したままで長さが 1.0 のものが作成されます。
            var direction = new Vector3(targetX - currentX, 0, 0).normalized;
            //directionの方向に0.1f移動する
            spriteTransform
                .DOLocalJump(direction * 0.1f, 0.1f, 1, time)
                .SetEase(Ease.Linear)
                .SetLink(gameObject);
            //0.1秒後に元の位置に戻る
            spriteTransform
                .DOLocalJump(Vector3.zero, 0.1f, 1, time)
                .SetEase(Ease.Linear)
                .SetDelay(time)
                .SetLink(gameObject);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            var take = other.GetComponent<Character>();
            if (take == null) return;
            _targets.Add(take);

            // ターゲットがいない場合は設定
            if (currentTarget == null) SetTarget();
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            var character = other.GetComponent<Character>();
            if (character == null) return;
            _targets.Remove(character);

            // ターゲット中のものがいなくなった場合はnullにする
            if (character == currentTarget)
            {
                SetTarget();
            }
        }

        private void SetTarget()
        {
            // ターゲットがいない場合はnullにする
            if (_targets.Count == 0) currentTarget = null;
            
            // 一番近いターゲットを取得
            var minDistance = float.MaxValue;
            foreach (var target in _targets)
            {
                var distance = (transform.position - target.transform.position).sqrMagnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    currentTarget = target;
                }
            }
        }
    }
}
