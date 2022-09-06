using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ZeroPrep.MineBuddies
{
    public class Gauge : MonoBehaviour
    {
        [SerializeField]
        float position = 0.5f;
        [SerializeField]
        Image needle;

        public float Position
        {
            set { position = Mathf.Clamp(value, 0, 1f); }
            get { return position; }
        }

        // Update is called once per frame
        void Update()
        {
            needle.transform.rotation = Quaternion.Euler(0,0,45f-90f*position);
        }
    }
}