using UnityEngine;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    public class CarriageSceneInstaller : MonoInstaller
    {
        public HazardManagerGO hazardManagerGameObjectPrefab;
        public HazardIcons hazardIconsPrefab;
        
        [Inject] private GameSettings _gameSettings;
        public override void InstallBindings()
        {
            InstallEngineComponents();
            InstallHazardComponents();
            
        }

        private void InstallEngineComponents()
        {
            Container.Bind<EngineFuel>().FromNew().AsSingle();
            Container.Bind<EngineSpeed>().FromNew().AsSingle();
            Container.Bind<EngineOdometer>().FromNew().AsSingle();
        }

        private void InstallHazardComponents()
        {
            Container.Bind<HazardManager>().FromNew().AsSingle();
            Container.Bind<AvailableHazardTypes>().FromNew().AsSingle();
            Container.Bind<HazardSpawner>().FromNew().AsSingle()
                .WithArguments(_gameSettings.MinSpawnTime, _gameSettings.MaxSpawnTime);
            Container.Bind<HazardManagerGO>().FromComponentInNewPrefab(hazardManagerGameObjectPrefab).AsSingle();
            Container.Bind<HazardIcons>().FromComponentInNewPrefab(hazardIconsPrefab).AsSingle();

        }
    }
}