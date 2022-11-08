using UnityEngine;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    public class CarriageSceneInstaller : MonoInstaller
    {
        public HazardManagerGO hazardManagerGameObjectPrefab;
        
        [Inject] private GameSettings _gameSettings;
        public override void InstallBindings()
        {
            InstallEngineComponents();
            InstallHazardComponents();
            
        }

        private void InstallEngineComponents()
        {
            Container.Bind<EngineFuel>().FromNew().AsSingle().WithArguments(_gameSettings.EngineCapacity, _gameSettings.EngineStartingFuel, _gameSettings.EngineBurnRate);
        }

        private void InstallHazardComponents()
        {
            Container.Bind<HazardManager>().FromNew().AsSingle();
            Container.Bind<HazardManagerGO>().FromComponentInNewPrefab(hazardManagerGameObjectPrefab).AsSingle();
            Container.Bind<AvailableHazardTypes>().FromNew().AsSingle();
        }
    }
}