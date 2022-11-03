using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CotrolsSplash : MonoBehaviour
{

    public UnityEngine.UI.Selectable CloseButton;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnEnable()
        //had to do this because the close button wasn't getting automatically selected.
    {
        EventSystem.current.SetSelectedGameObject(CloseButton.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
