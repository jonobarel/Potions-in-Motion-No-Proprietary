using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTilemapNames : MonoBehaviour
{
    public Dictionary<string, Sprite> keySpritesDict;
    [Serializable]
    public struct KeyAndTile
    {
        public string name;
        public Sprite keyTile;
    }

    [SerializeField]
    public KeyAndTile[] keyMapArr;

    public void Awake()
    {
        Debug.Log("Converting key array to dictrionary");
        keySpritesDict = new Dictionary<string, Sprite>();
        foreach (KeyAndTile k in keyMapArr)
        {
            keySpritesDict[k.name] = k.keyTile;
        }
    }

}
