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
        private bool _clearTriggerResult = false;
        private bool _advanceTriggerResult = false;
        
        /// <summary>
        /// Test <c>HazardHealthReachesZero</c> tests that when treating the Hazard, it will reach 0.
        /// </summary>
        [Test]
        public void HazardHealthReachesZero()
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
            Assert.Less(hzTest.Progress, 1f, "After partial progress, Progress should be less than 1");
            Assert.Greater(hzTest.Progress,0f, "after partial progress, Progress should be gt 0");
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
        
        [Test]
        public void HazardTreatmentTriggersEvent()
        {
            HazardBase.OnTreat += TestTreatmentEventTrigger;
            HazardExternal hzTreat = new HazardExternal(0.5f, Managers.HazardType.B);
            hzTreat.Treat(0.5f);
            Assert.IsTrue(_treatmentTriggerResult);
            HazardBase.OnTreat -= TestTreatmentEventTrigger;
        }

        /// <summary>
        /// Method to be added to OnExpire.
        /// </summary>
        /// <param name="h">the Hazard that triggers the event.</param>
        public void TestExpireEventTrigger(HazardBase h)
        {
            _expireTriggerResult = true;
            Assert.NotNull(h);
            HazardExternal hzTest = (HazardExternal)h;
            
            Assert.AreEqual(hzTest.Type, Managers.HazardType.C);
        }
        
        [Test]
        public void HazardProgressTriggersExpireEvent()
        {
            HazardBase.OnExpire += TestExpireEventTrigger;
            HazardExternal hzExpire = new HazardExternal(1f, Managers.HazardType.C);
            hzExpire.Advance(1f);
            Assert.IsTrue(_expireTriggerResult);
            HazardBase.OnExpire -= TestExpireEventTrigger;
        }

        public void TestOnClearEvent(HazardBase h)
        {
            Assert.NotNull(h);
            HazardExternal hz = (HazardExternal)h;
            Assert.IsTrue(hz.Health <=0f);
            _clearTriggerResult = true;
        }
        
        [Test]
        public void HazardClearTriggersEvent()
        {
            HazardBase.OnClear += TestOnClearEvent;

            HazardExternal hzTest = new HazardExternal(1f, Managers.HazardType.C);
            hzTest.Treat(0.5f);
            Assert.IsFalse(_clearTriggerResult);
            hzTest.Treat(10f);
            Assert.IsTrue(_clearTriggerResult);
            HazardBase.OnClear -= TestOnClearEvent;
        }

        public void HazardAdvanceTriggerMethod(HazardBase h)
        {
            Assert.NotNull(h);
            HazardExternal hzTest = (HazardExternal)h;

            Assert.AreEqual(hzTest.Type, Managers.HazardType.D, "mismatched hazard type");

            _advanceTriggerResult = true;

        }
        [Test]
        public void HazardAdvanceTriggersEvent()
        {
            HazardBase.OnAdvance += HazardAdvanceTriggerMethod;
            HazardExternal hzTest = new HazardExternal(1f, Managers.HazardType.D);
            hzTest.Advance(0.1f);
            Assert.IsTrue(_advanceTriggerResult, "Event was not triggered");
            HazardBase.OnAdvance -= HazardAdvanceTriggerMethod;

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