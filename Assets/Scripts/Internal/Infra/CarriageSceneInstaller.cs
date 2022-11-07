using UnityEngine;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    public class CarriageSceneInstaller : MonoInstaller
    {
        
        public override void InstallBindings()
        {
            Container.Bind<Engine>().FromNew().AsSingle().WithArguments(100f, 90f, 2f);
        }
    }
}