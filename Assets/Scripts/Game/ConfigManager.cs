using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public class ConfigManager : ManagerBase
    {
        public MineBuddiesConfigFile config;
        public void Start()
        {
            //DontDestroyOnLoad(this);
        }
    }
}