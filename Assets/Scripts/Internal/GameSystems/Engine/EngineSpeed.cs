using Zenject;

namespace ZeroPrep.MineBuddies
{
    public class EngineSpeed
    {
        private GameSettings _gameSettings;
        
        private EngineFuel _engineFuel;

        public EngineSpeed(EngineFuel engineFuel, GameSettings gameSettings)
        {
            _engineFuel = engineFuel;
            _gameSettings = gameSettings;
        }

        public float CurrentSpeed()
        {
            //TODO - calculate speed function for EngineSpeed.
            return 10f;
        }
    }
}