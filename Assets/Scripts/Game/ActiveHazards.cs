using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        }


        public Hazard FindTop(GameManager.HazardType t)
        {
            var h_list = (from hazard in activeHazardsList
                     where hazard.type == t
                     orderby hazard.gameObject.transform.GetSiblingIndex() ascending
                     select hazard);
            if (h_list.Count<Hazard>() == 0) return null;
            else return h_list.First();
            
                
        }

    }
}