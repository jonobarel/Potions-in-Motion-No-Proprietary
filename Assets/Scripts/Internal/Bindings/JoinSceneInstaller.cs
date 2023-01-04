using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

namespace ZeroPrep.MineBuddies
{
    public class JoinSceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerSkinsList playerSkinsList;
        [SerializeField] PlayerJoinManagement _playerJoinManagement;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerJoinManagement>().FromInstance(_playerJoinManagement).AsSingle();
            Container.Bind<PlayerSkinsList>().FromInstance(playerSkinsList).AsSingle();

        }
    }
}