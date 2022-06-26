using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePadReader : MonoBehaviour
{
    public TextMeshProUGUI textlabel;
    // Start is called before the first frame update
    void Start()
    {
        textlabel = FindObjectOfType<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textlabel.text = "";
        
        if (Input.GetButton("Joy0"))
        {
            textlabel.text += "Joystick Button 0\n";
        }
        if (Input.GetButton("Joy1"))
        {
            textlabel.text += "Joystick Button 1\n";
        }


        if (Input.GetButton("Joy2"))
        {
            textlabel.text += "Joystick Button 2\n";
        }
        if (Input.GetButton("Joy3"))
        {
            textlabel.text += "Joystick Button 3\n";
        }
        if (Input.GetButton("Joy4"))
        {
            textlabel.text += "Joystick Button 4\n";
        }
        if (Input.GetButton("Joy5"))
        {
            textlabel.text += "Joystick Button 5\n";
        }
        if (Input.GetButton("Joy6"))
        {
            textlabel.text += "Joystick Button 6\n";
        }
        if (Input.GetButton("Joy7"))
        {
            textlabel.text += "Joystick Button 7\n";
        }
        if (Input.GetButton("Joy8"))
        {
            textlabel.text += "Joystick Button 8\n";
        }
        if (Input.GetButton("Joy9"))
        {
            textlabel.text += "Joystick Button 9\n";
        }
        if (Input.GetButton("Joy10"))
        {
            textlabel.text += "Joystick Button 10";
        }
    }
}
