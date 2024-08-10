using System;
using System.Collections.Generic;
using DG.Tweening;
using Main.Scripts.Configs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Gameplay
{
    public class CardReshuffle
    {
        private GameAnimationTimeConfig _animationTimeConfig;
        
        Sequence _sequence;
        
        public CardReshuffle(GameAnimationTimeConfig animationTimeConfig)
        {
            _animationTimeConfig = animationTimeConfig;
        }
        public void ReshuffleCardPositions(List<Card> cards,  Action onComplete)
        {
            List<Vector3> originalPositions = new List<Vector3>();
            foreach (var card in cards)
            {
                originalPositions.Add(card.transform.position);
            }
            Shuffle(originalPositions);
             _sequence = DOTween.Sequence();
        
            for (int i = 0; i < cards.Count; i++)
            {
                _sequence.Join(cards[i].transform.DOMove(originalPositions[i], _animationTimeConfig.cardReshuffleTime));
            }
        
            _sequence.OnComplete(() => onComplete?.Invoke());
        }

        private void Shuffle<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T temp = list[i];
                int randomIndex = Random.Range(0, list.Count);
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }
    }

}