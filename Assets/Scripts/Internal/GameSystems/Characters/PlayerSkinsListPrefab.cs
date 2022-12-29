using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public class PlayerSkinsListPrefab : MonoBehaviour
    {
        [SerializeField] private Sprite[] playerSkins;

        public List<Sprite> PlayerSkins => playerSkins.ToList();
    }
}