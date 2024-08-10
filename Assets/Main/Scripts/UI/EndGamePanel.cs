using System;
using DG.Tweening;
using Main.Scripts.Configs;
using Main.Scripts.Saving;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Main.Scripts.UI
{
    public class EndGamePanel : MonoBehaviour
    {
        [SerializeField] private GameObject textsBg;
        [SerializeField] private TextMeshProUGUI levelStatusText;
        [SerializeField] private TextMeshProUGUI rewardText;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button menuButton;

        private int _successReward;
        private GameAnimationTimeConfig _animationTimeConfig;
        private SaveSystem _saveSystem;
        private SceneLoader _sceneLoader;
        private Sequence _sequence;

        [Inject]
        public void Construct(GameAnimationTimeConfig animationTimeConfig, SaveSystem saveSystem, SceneLoader sceneLoader)
        {
            _animationTimeConfig = animationTimeConfig;
            _saveSystem = saveSystem;
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            restartButton.onClick.AddListener(OnRestartButtonClicked);
            menuButton.onClick.AddListener(OnMenuButtonClicked);
        }
        
        private void OnRestartButtonClicked()
        {
            _sceneLoader.LoadScene("Game");
        }
        
        private void OnMenuButtonClicked()
        {
            _sceneLoader.LoadScene("Menu");
        }

        public void ShowEndGamePanel(bool isWin)
        {
            int reward = _saveSystem.LoadCardsToSpawn() * 10;
            _successReward = isWin ? reward : 0;
            levelStatusText.text = isWin ? "Level complete!" : "Level failed!";
            rewardText.text = $"Score: +{_successReward}";
            PlayScaleAnimation();
            
            int currentScore = _saveSystem.LoadScore();
            _saveSystem.SaveScore(currentScore + _successReward);
        }

        private void PlayScaleAnimation()
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(textsBg.transform.DOScale(1, _animationTimeConfig.endGamePanelScaleTime));
        }
    }
}