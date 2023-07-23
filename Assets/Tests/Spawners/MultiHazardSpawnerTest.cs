using NUnit.Framework;

namespace ZeroPrep.MineBuddies.Tests
{
    public class MultiHazardSpawnerTest
    {

        
        [Test]
        public void SpawnRandomTypeHazard_NoArgs_ShouldSpawnPlainHazard()
        {
            AvailableHazardTypes availableHazardTypes = new AvailableHazardTypes();
            HazardSpawnerMultiType spawner = new HazardSpawnerMultiType(1f,1f, availableHazardTypes, null);
            
            HazardMultiType hazard = (HazardMultiType)spawner.SpawnRandomTypeHazard();

            Assert.True(hazard.Types.Length == 1);
            Assert.True(hazard.InteractionTypes.Length == 1);
            Assert.True(hazard.Segments == 1);
        }

        [Test]
        public void SpawnRandomTypeHazard_WithNum_ShouldHaveTotalEqualToNum()
        {
            AvailableHazardTypes availableHazardTypes = new AvailableHazardTypes();
            HazardSpawnerMultiType spawner = new HazardSpawnerMultiType(1f,1f, availableHazardTypes, null);

            for (int i = 2; i < availableHazardTypes.Types.Length; i++)
            {
                HazardMultiType hazard = (HazardMultiType)spawner.SpawnRandomTypeHazard(i);
                
                Assert.True(hazard.InteractionTypes.Length + hazard.Segments == i);
            }
            

        }
        
    }
}