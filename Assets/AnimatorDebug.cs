using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorDebug : MonoBehaviour
{
    private Animator anim;

    private bool previousState;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        previousState = anim.GetBool("Activating");
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("Activating") != previousState)
        {
            Debug.Log("Activating changed to " + anim.GetBool("Activating"));
            previousState = anim.GetBool("Activating");
        }
    }
}
