using System;
using Main.Scripts.Saving;
using TMPro;
using UnityEngine;
using Zenject;

namespace Main.Scripts.UI
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        private int _score;
        private SaveSystem _saveSystem;
        
        [Inject]
        public void Construct(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }

        private void Start()
        {
            _score = _saveSystem.LoadScore();
            UpdateScoreText();
        }
        

        private void UpdateScoreText()
        {
            scoreText.text = $"Score: {_score}";
        }
    }
}