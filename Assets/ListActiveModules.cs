using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ZeroPrep.MineBuddies;

public class ListActiveModules : MonoBehaviour
{
    // Start is called before the first frame update
    private Module _module;
    TextMeshProUGUI _text;
    void Start()
    {
        _module = FindObjectOfType<Module>();
        _text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = "";
        foreach (var interaction in _module.ModuleActivations)
        {
            _text.text += interaction.GetType().Name + ": " + interaction.Activable + "\n";
        }
    }
}
