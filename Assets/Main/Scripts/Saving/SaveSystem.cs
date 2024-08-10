namespace Main.Scripts.Saving
{
    public class SaveSystem
    {
        private readonly IStorageService _storageService;
        private const string ScoreKey = "Score";
        private const string CardsToSpawnKey = "CardsToSpawn";
        private const string MusicEnabledKey = "MusicEnabled";

        public SaveSystem(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public void SaveScore(int score)
        {
            _storageService.Save(ScoreKey, score);
        }

        public int LoadScore()
        {
            return _storageService.Load<int>(ScoreKey, 0);
        }

        public void SaveCardsToSpawn(int cardsToSpawn)
        {
            _storageService.Save(CardsToSpawnKey, cardsToSpawn);
        }

        public int LoadCardsToSpawn()
        {
            return _storageService.Load<int>(CardsToSpawnKey, 4);
        }

        public void SaveSoundEnabled(bool isEnabled)
        {
            _storageService.Save(MusicEnabledKey, isEnabled);
        }

        public bool LoadSoundEnabled()
        {
            return _storageService.Load<bool>(MusicEnabledKey, true);
        }
    }
}