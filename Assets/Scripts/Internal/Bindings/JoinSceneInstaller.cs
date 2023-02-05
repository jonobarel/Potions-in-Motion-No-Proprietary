using UnityEngine;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    public class JoinSceneInstaller : MonoInstaller
    {

        [SerializeField] PlayerConfigManagement playerConfigManagement;
        [SerializeField] private GameObject playerJoinContainer;
        [SerializeField] private GameObject allPlayersReady;
        public override void InstallBindings()
        {
            Container.Bind<PlayerConfigManagement>().FromInstance(playerConfigManagement).AsSingle();

            Container.Bind<GameObject>().WithId("PlayerJoinContainer").FromInstance(playerJoinContainer);
            Container.Bind<GameObject>().WithId("AllPlayersReadyUI").FromInstance(allPlayersReady);
        }
    }
}