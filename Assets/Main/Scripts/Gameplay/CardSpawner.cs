using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Main.Scripts.Gameplay
{
    public class CardSpawner : MonoBehaviour
    {
        [SerializeField] private Card cardPrefab;
        [SerializeField] private Transform parent;
        [SerializeField] private List<Sprite> cardSprites;
        
        private List<Card> _cards = new();
        
        public void SpawnCards(DiContainer container, int cardCount)
        {
            List<Sprite> selectedSprites = new List<Sprite>();
            for (int i = 0; i < cardCount / 2; i++)
            {
                selectedSprites.Add(cardSprites[i % cardSprites.Count]);
                selectedSprites.Add(cardSprites[i % cardSprites.Count]);
            }

            Shuffle(selectedSprites);

            foreach (Sprite sprite in selectedSprites)
            {
                Card card = container.InstantiatePrefabForComponent<Card>(cardPrefab, parent);
                card.SetCardFace(sprite);
                _cards.Add(card);
            }
        }
        
        public List<Card> GetCardsOnBoard() => _cards;

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