using Main.Scripts.Saving;
using UnityEngine;
using Zenject;

namespace Main.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoader sceneLoader;
        [SerializeField] private SoundEffects soundEffects;
        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().FromInstance(sceneLoader).AsSingle();
            Container.Bind<SoundEffects>().FromInstance(soundEffects).AsSingle();
            
            Container.Bind<IStorageService>().To<PlayerPrefsStorageService>().AsSingle();
            // Container.Bind<IStorageService>().To<JsonFileStorageService>().AsSingle();
            //Container.Bind<IStorageService>().To<Base64StorageService>().AsSingle();
            
            Container.Bind<SaveSystem>().FromMethod(context => new SaveSystem(context.Container.Resolve<IStorageService>())).AsSingle();
        }
    }
}