using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconDisplay : MonoBehaviour
{
    public GameObject iconSprite;
    
    public void Show()
    {
        iconSprite.SetActive(true);
    }
    public void Hide()
    {
        iconSprite.SetActive(false);
    }
}
