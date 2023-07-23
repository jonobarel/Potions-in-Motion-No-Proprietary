using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Zenject.SpaceFighter;

namespace ZeroPrep.MineBuddies
{
    public class CarriageSceneInstaller : MonoInstaller
    {
        public HazardManagerGO hazardManagerGameObjectPrefab;
        public PlayerConfigManagement PlayerConfigManagementPrefab;
        public ScrollingBackground scrollingBackground;
        
        public bool ignoreFuel = false;
        
        [Inject] private GameSettings _gameSettings;
        public override void InstallBindings()
        {
            InstallGameComponents();
            InstallEngineComponents();
            InstallHazardComponents();
            InstallPlayers();
            InstallBackground();

        }

        private void InstallGameComponents()
        {
            Container.Bind<MineBuddiesMultiplayerLevelManager>()
                .FromInstance(FindObjectOfType<MineBuddiesMultiplayerLevelManager>());
        }
        private void InstallEngineComponents()
        {
            Container.Bind<EngineFuel>().FromInstance(new EngineFuel(_gameSettings, ignoreFuel)).AsSingle();
            Container.Bind<EngineSpeed>().FromNew().AsSingle();
            
            
            Container.Bind<EngineOdometer>().FromNew().AsSingle();
        }

        private void InstallHazardComponents()
        {
            Container.Bind<HazardManager>().FromNew().AsSingle();
            Container.Bind<HazardTypesActiveInGame>().FromNew().AsSingle();
            
            Container.Bind<HazardSpawner>().FromNew().AsSingle()
                .WithArguments(_gameSettings.MinSpawnTime, _gameSettings.MaxSpawnTime);
            Container.Bind<HazardManagerGO>().FromComponentInNewPrefab(hazardManagerGameObjectPrefab).AsSingle();
        }

        private void InstallPlayers()
        {
            PlayerConfigManagement playerConfigManagement = FindObjectOfType<PlayerConfigManagement>();
            if (playerConfigManagement is null)
            {
                Container.Bind<PlayerConfigManagement>().FromComponentInNewPrefab(PlayerConfigManagementPrefab).AsSingle();
                Container.Bind<GameObject>().WithId("PlayerJoinContainer").FromInstance(null);
                Container.Bind<GameObject>().WithId("AllPlayersReadyUI").FromInstance(null);
                Container.Bind<PlayerConfigManagement.PlayState>().FromInstance(PlayerConfigManagement.PlayState.GAME);
            }
            else
            {
                Container.Bind<PlayerConfigManagement>().FromInstance(playerConfigManagement).AsSingle();
            }
            PlayerInput[] players = FindObjectsOfType<PlayerInput>();
            Container.Bind<PlayerInput[]>().FromInstance(players).AsSingle();
        }

        private void InstallBackground()
        {

            if (scrollingBackground != null)
            {
                Container.Bind<ScrollingBackground>().FromInstance(scrollingBackground);    
            }
            
        }
    }
}