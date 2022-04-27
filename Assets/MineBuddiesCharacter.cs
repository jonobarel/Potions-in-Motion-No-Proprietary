using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baltamstudios.minebuddies
{
    public class MineBuddiesCharacter : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            transform.parent = Carriage.Instance.transform;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}