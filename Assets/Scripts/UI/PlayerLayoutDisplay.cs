using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLayoutDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public string PlayerID;

    [Serializable]
    public struct keyStruct
    {
        public string name;
        public Image keyTile;
        public string key;
    }

    public keyStruct[] Keys;
    
    KeyTilemapNames keyTileMap;
    public TextMeshProUGUI playerLabel;
    
    void Start()
    {
        keyTileMap = FindObjectOfType<KeyTilemapNames>();
        if (keyTileMap is null)
        {
            Debug.LogError("Couldn't find KeyTilemapNames object in order to update player keys. Make sure it is in the scene.");
            Application.Quit();
        }
        
        foreach (keyStruct k in Keys)
        {
            k.keyTile.sprite = keyTileMap.keySpritesDict[k.key];
        }

        playerLabel.text = gameObject.name;
    }

    
}
