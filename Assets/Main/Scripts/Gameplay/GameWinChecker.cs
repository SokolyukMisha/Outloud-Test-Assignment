using System;
using System.Collections.Generic;

namespace Main.Scripts.Gameplay
{
    public class GameWinChecker
    {
        private readonly CardMatcher _cardMatcher;
        private List<Card> _cardsOnBoard;
        public event Action OnWin;

        public GameWinChecker(CardMatcher cardMatcher)
        {
            _cardMatcher = cardMatcher;
        }

        public void StartChecking(List<Card> cardsOnBoard)
        {
            _cardsOnBoard = cardsOnBoard;
            _cardMatcher.OnMatchFound += CheckWinCondition;
        }

        private void CheckWinCondition()
        {
            foreach (Card card in _cardsOnBoard)
            {
                if (card.IsFaceDown())
                {
                    return;
                }
            }

            OnWin?.Invoke();
        }
    }
}