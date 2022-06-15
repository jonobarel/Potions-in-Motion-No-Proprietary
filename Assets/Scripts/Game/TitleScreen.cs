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
        if (Input.GetButtonDown("Player1_Jump") || 
            Input.GetButtonDown("Player2_Jump") ||
            Input.GetButtonDown("Player3_Jump") ||
            Input.GetButtonDown("Player4_Jump")
            )
        {
            MMSceneLoadingManager.LoadScene("MineBuddiesCharacterJoin");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<PauseButton>().PauseButtonAction();
        }

    }
}
