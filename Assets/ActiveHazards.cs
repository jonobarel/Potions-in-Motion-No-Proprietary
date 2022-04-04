using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.baltamstudios.minebuddies
{
    public class ActiveHazards : MonoBehaviour
    {
        List<Hazard> activeHazardsList = new List<Hazard>();
        [SerializeField] 
        VerticalLayoutGroup activeHazardQueueUI;

        [SerializeField]
        ActiveHazardUI activeHazardUIPrefab;
        public int Count { get { return activeHazardsList.Count; } }

        public void Add(Hazard h)
        {
            activeHazardsList.Add(h);
            //Instantiate the UI for the new Hazard.
            ActiveHazardUI uiDisplay = Instantiate(activeHazardUIPrefab, activeHazardQueueUI.transform);
            uiDisplay.transform.SetAsLastSibling();
            uiDisplay.ActiveHazardObj = h;
            h.IsActive = true;
        }



    }
}