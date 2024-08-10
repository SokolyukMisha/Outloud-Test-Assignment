using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Main.Scripts.Gameplay
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;

        public event Action OnTimeUp;

        private float _gameDuration;
        private float _remainingTime;
        private Coroutine _timerCoroutine;
        private bool _isRunning;

        public void StartTimer(float duration)
        {
            _gameDuration = duration;
            _remainingTime = _gameDuration;
            _isRunning = true;
            _timerCoroutine = StartCoroutine(TimerCoroutine());
        }

        public void SetTimerStatus(bool isRunning) => _isRunning = isRunning;

        private IEnumerator TimerCoroutine()
        {
            while (_remainingTime > 0 && _isRunning)
            {
                _remainingTime -= Time.deltaTime;
                timerText.text = TimeSpan.FromSeconds(_remainingTime).ToString(@"mm\:ss");
                yield return null;
            }

            if (_isRunning)
            {
                OnTimeUp?.Invoke();
            }
        }
    }
}