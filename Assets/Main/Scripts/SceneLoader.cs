using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.Scripts
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        
        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            GameObject loadingScreen = Instantiate(canvas);
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            Destroy(loadingScreen);
        }
    }
}