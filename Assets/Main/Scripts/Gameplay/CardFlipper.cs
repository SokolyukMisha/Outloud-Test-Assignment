using System.Collections;
using Main.Scripts.Configs;
using UnityEngine;
using Zenject;

namespace Main.Scripts.Gameplay
{
    public class CardFlipper : MonoBehaviour
    {
        private Card _firstCard;
        private Card _secondCard;
        private CardMatcher _cardMatcher;
        private GameAnimationTimeConfig _animationTimeConfig;
        private Coroutine _flipCoroutine;
        
        [Inject]
        public void Construct(CardMatcher cardMatcher, GameAnimationTimeConfig animationTimeConfig)
        {
            _cardMatcher = cardMatcher;
            _animationTimeConfig = animationTimeConfig;
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if(hit.collider.TryGetComponent(out Card card))
                        {
                            HandleCardFLip(card);
                        }
                    }
                }
            }
        }

        private void HandleCardFLip(Card card)
        {
            if(!card.IsFaceDown()) return;
            
            if (_firstCard == null)
            {
                _firstCard = card;
                _firstCard.Flip();
            }
            else if (_secondCard == null)
            {
                _secondCard = card;
                _secondCard.Flip();
                _flipCoroutine = StartCoroutine(CheckForMatchCo());
            }
        }
        
        private IEnumerator CheckForMatchCo()
        {
            yield return new WaitForSeconds(_animationTimeConfig.waitForCheckMatch);
            _cardMatcher.CheckForMatch(_firstCard, _secondCard);
            _firstCard = null;
            _secondCard = null;
        }
    }
}