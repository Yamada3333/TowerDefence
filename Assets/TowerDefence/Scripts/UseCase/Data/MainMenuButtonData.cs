using UnityEngine;

namespace TowerDefence.Scripts.UseCase.Data
{
    public class MainMenuButtonData
    {
        public string ButtonText;
        public string SceneName;
        public Sprite Sprite;
        
        public MainMenuButtonData(Sprite sprite, string buttonText, string sceneName)
        {
            Sprite = sprite;
            ButtonText = buttonText;
            SceneName = sceneName;
        }
    }
}
