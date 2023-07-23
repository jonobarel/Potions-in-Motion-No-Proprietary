using System;
using System.Collections.Specialized;
using System.Linq;
using Zenject;
using NUnit.Framework;
using UnityEngine;
using static ZeroPrep.Utils.Helpers;
using GameSettingsInstaller = ZeroPrep.MineBuddies.GameSettingsInstaller;


namespace ZeroPrep.MineBuddies.Tests
{
    [TestFixture]
    public class MultiHazardTest : ZenjectUnitTestFixture
    {
        
        private GameSettings _settings;

        private int _numModules = 4;
        private Module[] modules;
        private HazardSpawnerMultiType spawner;
        private HazardTypesActiveInGame _hazardTypesActiveInGame;

        private HazardManagerGO.InteractionType[] _interactionTypes =
            (HazardManagerGO.InteractionType[])Enum.GetValues(typeof(HazardManagerGO.InteractionType));

        [SetUp]
        public void Init()
        {
            InstantiateModules();
            BindInterfaces();
            InitializeMembers();

        }

        private void InitializeMembers()
        {
            _hazardTypesActiveInGame = Container.Resolve<HazardTypesActiveInGame>();
            spawner = new HazardSpawnerMultiType(1, 1, _hazardTypesActiveInGame, null);
        }

        private void BindInterfaces()
        {
            
            TestInstaller.Install(Container);
            _settings = Container.Resolve<GameSettings>();

        }

        private void InstantiateModules()
        {
            var hazardTypes = HazardType.AvailableTypes;
            modules = new Module[_numModules];
            for (int i = 0; i < _numModules; i++)
            {
                GameObject moduleObj = new GameObject();
                Module module = moduleObj.AddComponent<Module>();
                module.HazardType = hazardTypes[i];
                modules[i] = module;
            }
        }

        [Test]
        public void AvailableHazardsShouldMatchModules()
        {
            Debug.Log(ArrayToString(_hazardTypesActiveInGame.Types));
            //Check available hazards initialized correctly
            Assert.NotNull(_hazardTypesActiveInGame);
            
            //check number of distinct hazard types is equal to number of modules
            Assert.True(_hazardTypesActiveInGame.Types.Distinct().Count() == _numModules);
            
            //check each hazard type in modules is in the available types list
            foreach (var module in modules)
            {
                Assert.True(_hazardTypesActiveInGame.Types.Contains(module.HazardType));
            }
        }
        
        
        /// <summary>
        /// Test that a multi-type hazard is created with two different types found in the AvailableHazardTypes list.
        /// </summary>
        [Test]
        public void HazardTypes__Complexity4_ShouldHave2DistinctHazardTypes()
        {
            HazardMultiType hazard = (HazardMultiType)spawner.SpawnRandomTypeHazard(5,2) as HazardMultiType;
            Debug.Log(ArrayToString(hazard.Types));
            Assert.True(hazard.Types.Distinct().Count() == 2);
        }
        

        [Test]
        public void TreatAction_lessThanSectionLength_shouldNotSwitchHazardType()
        {
            HazardMultiType hazard = (HazardMultiType)spawner.SpawnRandomTypeHazard(5) as HazardMultiType;

            int currentIndex = 0;
            Assert.True(hazard.Type == hazard.Types[currentIndex]);
            //applying half segment length treatment
            hazard.TreatAction(0.5f*hazard.SectionLength);
            Assert.True(hazard.Type == hazard.Types[currentIndex]);
        }
        
        
        [Test]
        public void TreatAction_sectionLength_shouldSwitchHazardType()
        {
            
            HazardMultiType hazard = (HazardMultiType)spawner.SpawnRandomTypeHazard(5) as HazardMultiType;

            int currentIndex = 0;

            //applying one segment length treatment
            hazard.TreatAction(hazard.SectionLength);
            currentIndex++;
            Assert.True(hazard.Type == hazard.Types[currentIndex]);
        }

        [Test]
        public void TreatAction_sectionLength_shouldSwitchInteractionType()
        {
            HazardMultiType hazard = (HazardMultiType)spawner.SpawnRandomTypeHazard() as HazardMultiType;

            Shuffle(_interactionTypes);
            hazard.InteractionTypes = _interactionTypes.Take(hazard.Types.Length).ToArray();
            
            for (int i = 0; i < hazard.Segments; i++)
            {
                Assert.True(hazard.InteractionTypes[hazard.IndexNormalized] == _interactionTypes[hazard.IndexNormalized]);
                hazard.TreatAction(hazard.SectionLength);
            } 

        }
        
        [Test]
        public void HazardTreatAction_HighComplexity_ShouldInvokeOnFlip()
        {
            HazardMultiType hazard = (HazardMultiType)spawner.SpawnRandomTypeHazard(5) as HazardMultiType;
            bool onFlipInvoked = false;
            hazard.OnFlip += (h) => onFlipInvoked = true;
            hazard.TreatAction(hazard.SectionLength);
            Assert.True(onFlipInvoked);
        }

    }
}