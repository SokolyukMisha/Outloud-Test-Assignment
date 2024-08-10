using UnityEngine;
using Zenject;

namespace Main.Scripts.UI
{
    public class BootScene : MonoBehaviour
    {
        private SceneLoader _sceneLoader;

        [Inject]
        public void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            _sceneLoader.LoadScene("Menu");
        }
    }
}