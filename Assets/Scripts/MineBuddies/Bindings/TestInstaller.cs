using Zenject;

namespace ZeroPrep.MineBuddies.Tests
{
    public class TestInstaller : Installer<TestInstaller>
    {

        private string _testSettingsPath = "Installers/TestSettingsInstaller";

        public override void InstallBindings()
        {
            GameSettingsInstaller.InstallFromResource(_testSettingsPath, Container);
            InstallEngineComponents();
            InstallHazardComponents();
            
        }

       
        private void InstallHazardComponents()
        {
            Container.Bind<HazardManager>().FromNew().AsSingle();
            Container.Bind<HazardTypesActiveInGame>().FromNew().AsSingle();

            Container.Bind<HazardSpawnerMultiType>().FromNew().AsSingle();


        }

        private void InstallEngineComponents()
        {
                Container.Bind<EngineFuel>().FromNew().AsSingle();
                Container.Bind<EngineSpeed>().FromNew().AsSingle();

        }
    }
}