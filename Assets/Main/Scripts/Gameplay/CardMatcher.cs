using System;
using DG.Tweening;
using Main.Scripts.Configs;

namespace Main.Scripts.Gameplay
{
    public class CardMatcher
    {
        public event Action OnMatchFound;
        
        private readonly GameAnimationTimeConfig _animationTimeConfig;
        private readonly SoundEffects _soundEffects;
        
        private Sequence _sequence;
        
        public CardMatcher(GameAnimationTimeConfig animationTimeConfig, SoundEffects soundEffects)
        {
            _animationTimeConfig = animationTimeConfig;
            _soundEffects = soundEffects;
        }
        public void CheckForMatch(Card firstCard, Card secondCard)
        {
            if (firstCard.GetCardType() == secondCard.GetCardType())
            {
                _sequence = DOTween.Sequence();
                _sequence.Append(firstCard.transform.DOScale(0, _animationTimeConfig.matchScaleTime));
                _sequence.Join(secondCard.transform.DOScale(0, _animationTimeConfig.matchScaleTime));
                _sequence.AppendCallback(() =>
                {
                    OnMatchFound?.Invoke();
                    _soundEffects.PlayMatchSound();
                });
            }
            else
            {
                firstCard.Flip();
                secondCard.Flip();
            }
        }
    }
}