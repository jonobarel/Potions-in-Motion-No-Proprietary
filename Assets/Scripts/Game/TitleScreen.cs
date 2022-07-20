using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;

public class TitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
#if !UNITY_WEBGL
    void Start()
    {


        StartCoroutine(LoadTrailer());
    }

    // Update is called once per frame

    IEnumerator LoadTrailer()
    {
        yield return new WaitForSeconds(60f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("TrailerPlayer");
    }
#endif

}
