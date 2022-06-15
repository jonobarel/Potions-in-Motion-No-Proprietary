using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class MineBuddiesLevelLoader : MonoBehaviour
    {
        // Start is called before the first frame update

        public string LevelToLoad;


        public void LoadLevel()
        {
            MoreMountains.Tools.MMSceneLoadingManager.LoadScene(LevelToLoad);
        }
    }
}