using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

namespace com.baltamstudios.minebuddies
{
    public class TitleScreen : MonoBehaviour
    {
        private void Update()
        {
            if (Gamepad.current.aButton.wasPressedThisFrame)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("PlayerSelect");
            }
        }

    }
}