using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;

public class TitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<PauseButton>().PauseButtonAction();
        }
        else if (Input.anyKeyDown && !Input.GetMouseButtonDown(0))
            MMSceneLoadingManager.LoadScene("MineBuddiesCharacterJoin");
    }
}
