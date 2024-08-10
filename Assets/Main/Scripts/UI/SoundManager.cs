using Main.Scripts.Saving;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Main.Scripts.UI
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private Button soundButton;
        [SerializeField] private TextMeshProUGUI soundButtonText;
        
        private bool _isSoundOn = true;
        private SaveSystem _saveSystem;
        private SoundEffects _soundEffects;
        
        [Inject]
        public void Construct(SaveSystem saveSystem, SoundEffects soundEffects)
        {
            _saveSystem = saveSystem;
            _soundEffects = soundEffects;
        }
        
        private void Start()
        {
            _isSoundOn = _saveSystem.LoadSoundEnabled();
            UpdateSoundButton();
            soundButton.onClick.AddListener(OnSoundButtonClicked);
        }
        
        private void OnSoundButtonClicked()
        {
            _isSoundOn = !_isSoundOn;
            UpdateSoundButton();
            _saveSystem.SaveSoundEnabled(_isSoundOn);
            _soundEffects.SetSoundEnabled(_isSoundOn);
        }
        
        private void UpdateSoundButton()
        {
            soundButtonText.text = _isSoundOn ? "Sound: On" : "Sound: Off";
        }
    }
}