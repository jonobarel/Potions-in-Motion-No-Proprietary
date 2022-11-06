namespace ZeroPrep.MineBuddies
{
    public class Engine
    {
        public float FuelCapacity { get; }

        public float BurnRate { get; }
        private float _burnRate;
        
        public float CurrentFuel { get; }
        private float _currentFuel;
        
        public float FuelLevel
        {
            get { return CurrentFuel / FuelCapacity; }
        }

        public Engine(float capacity, float startingFuel, float burnRate)
        {
            FuelCapacity = capacity;
            CurrentFuel = startingFuel;
            BurnRate = burnRate;
        }

        public bool HasFuel(float amount)
        {
            return CurrentFuel >= amount;
        }

        public bool RequestFuel(float amount)
        {
            if (HasFuel(amount))
            {
                _currentFuel -= amount;
                return true;
            }

            return false;
        }
        
        
        
    }
}