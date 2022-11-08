using UnityEngine;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    public class CarriageSceneInstaller : MonoInstaller
    {
        [Inject] private GameSettings _gameSettings;
        public override void InstallBindings()
        {
            //Container.Bind<Engine>().FromNew().AsSingle().WithArguments(100f,100f,10f);
            Container.Bind<Engine>().FromNew().AsSingle().WithArguments(_gameSettings.EngineCapacity, _gameSettings.EngineStartingFuel, _gameSettings.EngineBurnRate);
            Container.Bind<HazardManagerGO>().FromInstance(FindObjectOfType<HazardManagerGO>());
        }
    }
}