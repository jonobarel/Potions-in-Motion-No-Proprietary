using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public abstract class ManagerBase : MonoBehaviour
    {
        // Start is called before the first frame update
        public virtual void LevelReset() { }


        public virtual void LevelEnd() { }
    }
}