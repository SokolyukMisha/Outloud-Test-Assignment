using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Configs;
using Main.Scripts.Saving;
using Main.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Main.Scripts.Gameplay
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private EndGamePanel endGamePanelPrefab;
        [SerializeField] private Transform endGamePanelParent;
        [SerializeField] private Button hintButton;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        
        private EndGamePanel _endGamePanel;
        private CardSpawner _cardSpawner;
        private GameAnimationTimeConfig _animationTimeConfig;
        private GameTimer _gameTimer;
        private GameWinChecker _gameWinChecker;
        private CardReshuffle _cardReshuffle;
        private SaveSystem _saveSystem;
        private DiContainer _container;
        private List<Card> _cardsOnBoard;

        [Inject]
        public void Construct(CardSpawner cardSpawner, GameAnimationTimeConfig animationTimeConfig, GameTimer gameTimer,
            GameWinChecker gameWinChecker, DiContainer container, CardReshuffle cardReshuffle, SaveSystem saveSystem)
        {
            _cardSpawner = cardSpawner;
            _animationTimeConfig = animationTimeConfig;
            _gameTimer = gameTimer;
            _gameWinChecker = gameWinChecker;
            _container = container;
            _cardReshuffle = cardReshuffle;
            _saveSystem = saveSystem;
        }

        private void Start()
        {
            InitEndGamePanel();
            InitBoard();
            InitTimer();
            InitWinChecker();
            InitHintButton();
        }

        private void InitHintButton()
        {
            hintButton.onClick.AddListener(ActivateHint);
            hintButton.interactable = true;
        }

        private void InitEndGamePanel()
        {
            _endGamePanel = _container.InstantiatePrefabForComponent<EndGamePanel>(endGamePanelPrefab, endGamePanelParent);
            _endGamePanel.gameObject.SetActive(false);
        }

        private void InitBoard()
        {
            int cardCount = _saveSystem.LoadCardsToSpawn();
            _cardSpawner.SpawnCards(_container, cardCount);
            _cardsOnBoard = _cardSpawner.GetCardsOnBoard();
            StartCoroutine(ShowCardsCo(_cardsOnBoard));
        }

        private void InitTimer()
        {
            _gameTimer.StartTimer(30);
            _gameTimer.OnTimeUp += () =>
            {
                _endGamePanel.gameObject.SetActive(true);
                _endGamePanel.ShowEndGamePanel(false);
                _gameTimer.SetTimerStatus(false);
            };
        }

        private void InitWinChecker()
        {
            _gameWinChecker.StartChecking(_cardsOnBoard);
            _gameWinChecker.OnWin += () =>
            {
                _endGamePanel.gameObject.SetActive(true);
                _gameTimer.SetTimerStatus(false);
                _endGamePanel.ShowEndGamePanel(true);
            };
        }

        private void ActivateHint()
        {
            gridLayoutGroup.enabled = false;
            hintButton.interactable = false;
            List<Card> cardsToFLip = _cardSpawner.GetCardsOnBoard().Where(card => card.IsFaceDown()).ToList();
            _cardReshuffle.ReshuffleCardPositions(cardsToFLip, OnComplete(cardsToFLip));
        }

        private Action OnComplete(List<Card> cardsToFlip)
        {
            return ()=>
            {
                gridLayoutGroup.enabled = true;
                StartCoroutine(ShowCardsCo(cardsToFlip));
            };
        }


        private IEnumerator ShowCardsCo(List<Card> cardsToShow)
        {
            foreach (Card card in cardsToShow)
            {
                card.Flip();
            }

            yield return new WaitForSeconds(_animationTimeConfig.cardsShowTime);
            foreach (Card card in cardsToShow)
            {
                card.Flip();
            }
            hintButton.interactable = true;
        }
    }
}