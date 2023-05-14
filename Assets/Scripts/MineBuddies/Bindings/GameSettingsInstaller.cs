using UnityEngine;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public GameSettings gameSettings;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameSettings>().FromInstance(gameSettings).AsSingle();
        }
    }
}