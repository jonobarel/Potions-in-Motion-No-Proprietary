using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class Helpers
    {
        public static MineBuddiesConfigFile Config { get { return GameSystem.Instance.configManager.config; } }
    }
}