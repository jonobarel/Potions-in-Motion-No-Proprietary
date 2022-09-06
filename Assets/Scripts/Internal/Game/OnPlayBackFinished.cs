using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnPlayBackFinished : MonoBehaviour
{
    // Start is called before the first frame update

    public void Start()
    {
        var videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();

        videoPlayer.loopPointReached += LoadTitleScreen;

        StartCoroutine(CountdownToLoadTitle());

    }


    public void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadTitleScreen(null);
        }
    }
    IEnumerator  CountdownToLoadTitle()
    {
        yield return new WaitForSeconds(52f);
        LoadTitleScreen(null);
        

    }

    public void LoadTitleScreen(UnityEngine.Video.VideoPlayer vp)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
