using System;
using Main.Scripts.Saving;
using UnityEngine;
using Zenject;

namespace Main.Scripts
{
    public class SoundEffects : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip cardFlipSound;
        [SerializeField] private AudioClip matchSound;
        [SerializeField] private AudioClip buttonClickSound;
        
        private SaveSystem _saveSystem;
        
        [Inject]
        public void Construct(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }

        private void Start()
        {
            bool isSoundOn = _saveSystem.LoadSoundEnabled();
            SetSoundEnabled(isSoundOn);
        }

        public void PlayButtonClickSound() => audioSource.PlayOneShot(buttonClickSound);
        public void PlayCardFlipSound() => audioSource.PlayOneShot(cardFlipSound);
        public void PlayMatchSound() => audioSource.PlayOneShot(matchSound);

        public void SetSoundEnabled(bool isSoundOn) => audioSource.mute = !isSoundOn;
    }
}