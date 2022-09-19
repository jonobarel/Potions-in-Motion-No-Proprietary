using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using ZeroPrep.MineBuddies;


namespace ZeroPrep.Minebuddies
{
    public class HazardExternalTests
    {
        private bool _treatmentTriggerResult = false;

        private bool _expireTriggerResult = false;
        
        /// <summary>
        /// Test <c>HazardHealthReachesZero</c> tests that when treating the Hazard, it will reach 0.
        /// </summary>
        [Test]
        public void HazardHeatlhReachesZero()
        {
            HazardExternal hzTest = new HazardExternal(0.5f, Managers.HazardType.A);
            hzTest.Treat(0.25f);
            Assert.AreEqual(hzTest.Health, 0.75f);
            hzTest.Treat(0.25f);
            Assert.AreEqual(hzTest.Health,0.5f);
        }

        /// <summary>
        /// Tests that when a progress grade is passed to the hazard, it will proceed towards 1 at rate <c>speed</c>
        /// </summary>
        [Test]
        public void HazardProgressReachesZero()
        {
            HazardExternal hzTest = new HazardExternal(0.5f, Managers.HazardType.A);
            hzTest.Advance(1f);
            Assert.AreEqual(hzTest.Progress, 0.5f);
            hzTest.Advance(1f);
            Assert.AreEqual(hzTest.Progress, 1.0f);
        }

        /// <summary>
        /// Method to be added to OnTreat
        /// </summary>
        /// <param name="h">the Hazard that triggers the event.</param>
        public void TestTreatmentEventTrigger(HazardBase h)
        {
            _treatmentTriggerResult = true;
            Assert.NotNull(h);
            Assert.AreEqual(((HazardExternal)h).Type, Managers.HazardType.B );
        }
        
        /// <summary>
        /// Method to be added to OnExpire.
        /// </summary>
        /// <param name="h">the Hazard that triggers the event.</param>
        public void TestExpireEventTrigger(HazardBase h)
        {
            _expireTriggerResult = true;
            Assert.NotNull(h);
            Assert.AreEqual(((HazardExternal)h).Type, Managers.HazardType.B);
        }
        
        [Test]
        public void HazardTreatmentTriggersEvent()
        {
            HazardBase.OnTreat += TestTreatmentEventTrigger;
            HazardExternal hzTreat = new HazardExternal(0.5f, Managers.HazardType.B);
            hzTreat.Treat(1f);
            Assert.IsTrue(_treatmentTriggerResult);
        }

        [Test]
        public void HazardProgressTriggersEvent()
        {
            HazardBase.OnExpire += TestExpireEventTrigger;
            HazardExternal hzExpire = new HazardExternal(1f, Managers.HazardType.B);
            hzExpire.Advance(1f);
            Assert.IsTrue(_expireTriggerResult);
        }
        /*
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator HazardExternalTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
        */
    }
}