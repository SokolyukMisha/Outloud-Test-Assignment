using DG.Tweening;
using Main.Scripts.Configs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Main.Scripts.Gameplay
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Image cardFace;

        private GameAnimationTimeConfig _animationTimeConfig;
        private SoundEffects _soundEffects;
        private bool _isFaceDown = true;
        private Sequence _flipSequence;
        
        [Inject]
        public void Construct(GameAnimationTimeConfig animationTimeConfig, SoundEffects soundEffects)
        {
            _animationTimeConfig = animationTimeConfig;
            _soundEffects = soundEffects;
        }

        public void SetCardFace(Sprite sprite) => cardFace.sprite = sprite;

        public Sprite GetCardType() => cardFace.sprite;
        
        public void Flip()
        {
            _soundEffects.PlayCardFlipSound();
            float halfTime = _animationTimeConfig.cardFlipTime / 2;
            _flipSequence = DOTween.Sequence();
            _flipSequence.Append(transform.DOLocalRotate(new Vector3(0, 90, 0), halfTime).SetEase(Ease.Linear));
            _flipSequence.AppendCallback(() => cardFace.gameObject.SetActive(!_isFaceDown));
            _flipSequence.Append(transform.DOLocalRotate(new Vector3(0, 0, 0), halfTime).SetEase(Ease.Linear));
            _isFaceDown = !_isFaceDown;
        }
        
        public bool IsFaceDown() => _isFaceDown;
    }
}