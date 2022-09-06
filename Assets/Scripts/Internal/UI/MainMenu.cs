using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()

    {   //Clicking the mouse in the window de-selects the menu items, I don't know why.
        //added this to ensure that something is always selected.
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(GetComponentInChildren<Selectable>().gameObject);
        }
    }
}
