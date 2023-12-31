using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;

namespace ZeroPrep.MineBuddies
{
    public class TitleScreen : MonoBehaviour
    {
        // Start is called before the first frame update

        public void Start()
        {
#if !UNITY_WEBGL
        StartCoroutine(LoadTrailer());
#endif
        }

        // Update is called once per frame

        IEnumerator LoadTrailer()
        {
            yield return new WaitForSeconds(60f);
            UnityEngine.SceneManagement.SceneManager.LoadScene("TrailerPlayer");
        }

    }
}