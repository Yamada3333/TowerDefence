using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Scripts.InGame.Model.Scriptable
{
    [CreateAssetMenu(fileName = "Stage", menuName = "Scriptable/Stage")]
    [Serializable]
    public class StageScriptable : ScriptableObject
    {
        // 敵のScriptableのリスト
        public List<CharacterScriptable> characterList = new();
        
        // 敵の出現時間のリスト
        public List<float> spawnTimeList = new();
    }
}
