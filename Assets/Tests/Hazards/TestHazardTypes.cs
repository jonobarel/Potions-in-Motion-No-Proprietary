using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using ZeroPrep.MineBuddies;

namespace ZeroPrep.MineBuddies.Tests
{
    public class TestHazardTypes
    {
        [Test]
        public void DefaultHazardType_NoArgs_ReturnDefault()
        {
            var hazardType = HazardType.DefaultHazardType;

            Assert.NotNull(hazardType);
            Assert.NotNull(hazardType.Glyph);
            Assert.IsTrue(hazardType.TypeID >= 0);
        }

        [Test]
        public void GetRandomHazardType_NoArgs_ReturnRandomType()
        {
            HazardType hazard = HazardType.GetRandomHazardType();
            Assert.NotNull(hazard);
            Assert.NotNull(hazard.Glyph);
            Assert.IsTrue(HazardType.AvailableTypes.Contains(hazard));
        }

        [Test]
        public void AvailableTypes_NotEmpty()
        {
            HazardType[] types = HazardType.AvailableTypes;

            Assert.NotNull(types);
            Assert.IsTrue(types.Length >= 1);
            Assert.IsTrue(types.Length == HazardType.GlyphAddresses.Length);
        }
        
        [Test]
        public void GetHazardType_int_ReturnHazardType()
        {
            int[] hazardIDs = HazardType.AvailableTypes.Select(x => x.TypeID).ToArray();
            
            foreach (int id in hazardIDs)
            {
                HazardType hazard = HazardType.GetHazardType(id);
                Assert.NotNull(hazard);
                Assert.IsTrue(hazard.TypeID == id);
                Assert.NotNull(hazard.Glyph);
                Assert.IsTrue(HazardType.AvailableTypes.Contains(hazard));
            }
        }

        [Test]
        public void GetHazardType_NonExistent_ReturnNull()
        {
            int nonExistentID = HazardType.AvailableTypes.Select(x => x.TypeID).Max() + 1;
            
            HazardType hazard = HazardType.GetHazardType(nonExistentID);
            Assert.IsNull(hazard);
        }


    }
}