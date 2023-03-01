using UnityEngine;
using Zenject;

namespace ZeroPrep.MineBuddies
{


    public class TestSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EngineFuel>().FromNew().AsSingle();
            Container.Bind<EngineSpeed>().FromNew().AsSingle();
            Container.Bind<EngineOdometer>().FromNew().AsSingle();
        }
    }
}