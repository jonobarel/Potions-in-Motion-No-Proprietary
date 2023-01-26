using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSelect : MonoBehaviour
{
    public TextMeshProUGUI ReadyLabel { get; private set; }

    public void Awake()
    {
        ReadyLabel = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetReady(bool isReady)
    {
        ReadyLabel.gameObject.SetActive(isReady);
    }
    
    

}
