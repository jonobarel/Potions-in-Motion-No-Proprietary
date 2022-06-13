using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
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