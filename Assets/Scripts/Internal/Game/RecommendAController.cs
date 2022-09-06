using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public class RecommendAController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine("GoToNextScreenDelay");
        }

        IEnumerator GoToNextScreenDelay()
        {
            yield return new WaitForSeconds(3f);

            Debug.Log("Timer expired");
            GoToNextScreen();
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.anyKeyDown)
            {
                GoToNextScreen();
            }
        }

        public void GoToNextScreen()
        {
            GetComponent<MoreMountains.Tools.MMLoadScene>().LoadScene();
        }
    }
}