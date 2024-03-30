using System;
using R3;
using UnityEngine;

namespace TowerDefence.Scripts.InGame.Model.Scriptable
{
    [CreateAssetMenu(fileName = "Character", menuName = "Scriptable/Character")]
    [Serializable]
    public class CharacterScriptable : ScriptableObject
    {
        public Sprite sprite;
        public int cost;
        
        public string characterName;
        public float health = 100.0f;
        public float attackPower = 10.0f;
        public float moveSpeed = 1.0f;
        public float attackSpeed = 3.0f;
        public float size = 1.0f;
        
        public Subject<Unit> OnSpawn = new();
    }
}
