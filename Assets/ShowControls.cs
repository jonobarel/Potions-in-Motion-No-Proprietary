using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ZeroPrepGames.TrollTruckerTales
{
    public class ShowControls : MonoBehaviour
    {
        public GameObject targetObject;
        // Start is called before the first frame update
        public void Show()
        {
            targetObject.SetActive(true);
        }

        public void Hide()
        {
            targetObject.SetActive(false);
        }
    }
}