using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Main.Scripts.UI
{
    public class LoadGameButton : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private string sceneName = "Game";
        
        private SceneLoader _sceneLoader;

        [Inject]
        public void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
        }
        
        private void OnPlayButtonClicked()
        {
            _sceneLoader.LoadScene(sceneName);
        }
    }
}