using UnityEngine;

namespace Main.Scripts.Configs
{
    [CreateAssetMenu(fileName = "GameAnimationTimeConfig", menuName = "ScriptableObjects/GameAnimationTimeConfig", order = 1)]
    public class GameAnimationTimeConfig : ScriptableObject
    {
        public float cardFlipTime = 1f;
        public float waitForCheckMatch = 1f;
        public float matchScaleTime = 0.5f;
        public float cardsShowTime = 0.5f;
        public float endGamePanelScaleTime = 0.5f;
        public float cardReshuffleTime = 0.5f;
    }
}