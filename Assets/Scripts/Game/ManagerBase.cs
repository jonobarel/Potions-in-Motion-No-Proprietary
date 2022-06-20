using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ZeroPrepGames.TrollTruckerTales
{
    public abstract class ManagerBase : MonoBehaviour
    {
        // Start is called before the first frame update
        public virtual void LevelReset() { }


        public virtual void LevelEnd() { }
    }
}