using Main.Scripts.Configs;
using Main.Scripts.Gameplay;
using UnityEngine;
using Zenject;

namespace Main.Scripts.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameAnimationTimeConfig animationTimeConfig;
        [SerializeField] private CardSpawner cardSpawner;
        [SerializeField] private GameTimer gameTimer;

        public override void InstallBindings()
        {
            Container.Bind<GameAnimationTimeConfig>().FromInstance(animationTimeConfig).AsSingle();
            Container.Bind<CardSpawner>().FromInstance(cardSpawner).AsSingle();
            Container.Bind<GameTimer>().FromInstance(gameTimer).AsSingle();

            Container.Bind<CardReshuffle>().FromMethod(context => new CardReshuffle(animationTimeConfig)).AsSingle();
            Container.Bind<CardMatcher>().FromMethod(context => new CardMatcher(animationTimeConfig, context.Container.Resolve<SoundEffects>())).AsSingle();
            Container.Bind<GameWinChecker>().FromMethod(context => new GameWinChecker(context.Container.Resolve<CardMatcher>())).AsSingle();
        }
    }


}