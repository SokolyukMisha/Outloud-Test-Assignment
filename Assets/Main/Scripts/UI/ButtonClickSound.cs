using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Main.Scripts.UI
{
    public class ButtonClickSound : MonoBehaviour
    {
        private SoundEffects _soundEffects;
        
        [Inject]
        public void Construct(SoundEffects soundEffects)
        {
            _soundEffects = soundEffects;
        }

        private void Start()
        {
            foreach (var button in FindObjectsOfType<Button>(true))
            {
                button.onClick.AddListener(_soundEffects.PlayButtonClickSound);
            }
        }
    }
}