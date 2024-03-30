using System;
using UnityEngine;

namespace TowerDefence.Scripts.Entity.Scriptable
{
    [CreateAssetMenu(fileName = "MainMenuButton", menuName = "Scriptable/MainMenuButton")]
    [Serializable]
    public class MainMenuButtonScriptable : ScriptableObject
    {
        public Sprite sprite;
        public string sceneName;
        public string buttonText;
    }
}
