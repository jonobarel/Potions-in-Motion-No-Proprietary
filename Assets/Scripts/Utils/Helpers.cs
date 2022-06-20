using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ZeroPrepGames.TrollTruckerTales
{
    public class Helpers
    {
        public static MineBuddiesConfigFile Config { get { if (GameSystem.Instance) return GameSystem.Instance.configManager.config; return null; } }
    }
}