using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Zenject.SpaceFighter;

namespace ZeroPrep.MineBuddies
{
    public class CarriageSceneInstaller : MonoInstaller
    {
        public HazardManagerGO hazardManagerGameObjectPrefab;
        public HazardIcons hazardIconsPrefab;
        public GameObject corgiCarriagePrefab;
        public PlayerConfigManagement PlayerConfigManagementPrefab;
        
        [Inject] private GameSettings _gameSettings;
        public override void InstallBindings()
        {
            InstallEngineComponents();
            InstallHazardComponents();
            InstallPlayers();

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
    }
}