using Main.Scripts.Saving;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Main.Scripts.UI
{
    public class CardsToSpawnController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI cardsToSpawnText;
        [SerializeField] private Slider cardsToSpawnSlider;
        
        private int _cardsToSpawn;
        private SaveSystem _saveSystem;

        [Inject]
        public void Construct(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }
        

        private void Start()
        {
            cardsToSpawnSlider.minValue = 2;
            cardsToSpawnSlider.maxValue = 15;
            cardsToSpawnSlider.wholeNumbers = true;
            int sliderValue = _saveSystem.LoadCardsToSpawn() / 2;
            cardsToSpawnSlider.value = sliderValue;

            UpdateCardsToSpawn((int)cardsToSpawnSlider.value * 2);

            cardsToSpawnSlider.onValueChanged.AddListener(OnSliderValueChanged);

            
        }

        private void OnSliderValueChanged(float value)
        {
            UpdateCardsToSpawn((int)value * 2);
        }

        private void UpdateCardsToSpawn(int value)
        {
            _cardsToSpawn = value;
            cardsToSpawnText.text = $"Cards to spawn: {_cardsToSpawn}";
            _saveSystem.SaveCardsToSpawn(_cardsToSpawn);
        }
        
    }
}