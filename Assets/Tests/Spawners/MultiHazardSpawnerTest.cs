using NUnit.Framework;

namespace ZeroPrep.MineBuddies.Tests
{
    public class MultiHazardSpawnerTest
    {

        
        [Test]
        public void SpawnRandomTypeHazard_NoArgs_ShouldSpawnPlainHazard()
        {
            HazardTypesActiveInGame hazardTypesActiveInGame = new HazardTypesActiveInGame();
            HazardSpawnerMultiType spawner = new HazardSpawnerMultiType(1f,1f, hazardTypesActiveInGame, null);
            
            HazardMultiType hazard = (HazardMultiType)spawner.SpawnRandomTypeHazard();

            Assert.True(hazard.Types.Length == 1);
            Assert.True(hazard.InteractionTypes.Length == 1);
            Assert.True(hazard.Segments == 1);
        }

        [Test]
        public void SpawnRandomTypeHazard_WithNum_ShouldHaveTotalEqualToNum()
        {
            HazardTypesActiveInGame hazardTypesActiveInGame = new HazardTypesActiveInGame();
            HazardSpawnerMultiType spawner = new HazardSpawnerMultiType(1f,1f, hazardTypesActiveInGame, null);

            for (int i = 2; i < hazardTypesActiveInGame.Types.Length; i++)
            {
                HazardMultiType hazard = (HazardMultiType)spawner.SpawnRandomTypeHazard(i);
                
                Assert.True(hazard.InteractionTypes.Length + hazard.Segments == i);
            }
            

        }
        
    }
}