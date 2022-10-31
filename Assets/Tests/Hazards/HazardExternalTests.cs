using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using ZeroPrep.MineBuddies;


namespace ZeroPrep.MineBuddies
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
            hzTest.TreatAction(0.25f);
            Assert.AreEqual(hzTest.Health, 0.75f);
            hzTest.TreatAction(0.25f);
            Assert.AreEqual(hzTest.Health,0.5f);
        }

        /// <summary>
        /// Tests that when a progress grade is passed to the hazard, it will proceed towards 1 at rate <c>speed</c>
        /// </summary>
        [Test]
        public void HazardProgressReachesZero()
        {
            HazardExternal hzTest = new HazardExternal(0.5f, Managers.HazardType.A);
            hzTest.AdvanceAction(1f);
            Assert.Less(hzTest.Progress, 1f, "After partial progress, Progress should be less than 1");
            Assert.Greater(hzTest.Progress,0f, "after partial progress, Progress should be gt 0");
            Assert.AreEqual(hzTest.Progress, 0.5f);
            hzTest.AdvanceAction(1f);
            Assert.AreEqual(hzTest.Progress, 1.0f);
        }

        /// <summary>
        /// Method to be added to OnTreat
        /// </summary>
        /// <param name="h">the Hazard that triggers the event.</param>
        public void TestTreatmentTrigger(HazardBase h)
        {
            _treatmentTriggerResult = true;
            Assert.NotNull(h);
            Assert.AreEqual(((HazardExternal)h).Type, Managers.HazardType.B );
        }
        
        [Test]
        public void HazardTreatmentTriggersEvent()
        {
            HazardBase.Treat += TestTreatmentTrigger;
            HazardExternal hzTreat = new HazardExternal(0.5f, Managers.HazardType.B);
            hzTreat.TreatAction(0.5f);
            Assert.IsTrue(_treatmentTriggerResult);
            HazardBase.Treat -= TestTreatmentTrigger;
        }

        /// <summary>
        /// Method to be added to OnExpire.
        /// </summary>
        /// <param name="h">the Hazard that triggers the event.</param>
        public void TestExpireTrigger(HazardBase h)
        {
            _expireTriggerResult = true;
            Assert.NotNull(h);
            HazardExternal hzTest = (HazardExternal)h;
            
            Assert.AreEqual(hzTest.Type, Managers.HazardType.C);
        }
        
        [Test]
        public void HazardProgressTriggersExpireEvent()
        {
            HazardBase.Expire += TestExpireTrigger;
            HazardExternal hzExpire = new HazardExternal(1f, Managers.HazardType.C);
            hzExpire.AdvanceAction(1f);
            Assert.IsTrue(_expireTriggerResult);
            HazardBase.Expire -= TestExpireTrigger;
        }

        public void TestClear(HazardBase h)
        {
            Assert.NotNull(h);
            HazardExternal hz = (HazardExternal)h;
            Assert.IsTrue(hz.Health <=0f);
            _clearTriggerResult = true;
        }
        
        [Test]
        public void HazardClearTriggersEvent()
        {
            HazardBase.Clear += TestClear;

            HazardExternal hzTest = new HazardExternal(1f, Managers.HazardType.C);
            hzTest.TreatAction(0.5f);
            Assert.IsFalse(_clearTriggerResult);
            hzTest.TreatAction(10f);
            Assert.IsTrue(_clearTriggerResult);
            HazardBase.Clear -= TestClear;
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
            HazardBase.Advance += HazardAdvanceTriggerMethod;
            HazardExternal hzTest = new HazardExternal(1f, Managers.HazardType.D);
            hzTest.AdvanceAction(0.1f);
            Assert.IsTrue(_advanceTriggerResult, "Event was not triggered");
            HazardBase.Advance -= HazardAdvanceTriggerMethod;

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