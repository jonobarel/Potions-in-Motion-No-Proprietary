using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace ZeroPrep.MineBuddies
{
    [RequireComponent(typeof(HazardManagerMono))]
    public class ActiveHazards : MonoBehaviour
    {
        List<HazardMono> activeHazardsList = new List<HazardMono>();
        [SerializeField] 
        VerticalLayoutGroup activeHazardQueueUI;

        public HazardManagerMono HazardManagerMono {
            get {
                if (_hazardManagerMono == null) _hazardManagerMono = GetComponent<HazardManagerMono>();
                if (_hazardManagerMono != null) return _hazardManagerMono;
                else Debug.LogError($"{name}: could not find HazardManager object");
                throw new System.Exception("HazardManager not found");
            } 
        }

        private HazardManagerMono _hazardManagerMono;
        

        [SerializeField]
        ActiveHazardUI activeHazardUIPrefab;
        public int Count { get { return activeHazardsList.Count; } }

        public void Add(HazardMono h)
        {
            if (!activeHazardsList.Contains(h))
            {
                activeHazardsList.Add(h);
                //Instantiate the UI for the new Hazard.
                ActiveHazardUI uiDisplay = Instantiate(activeHazardUIPrefab, activeHazardQueueUI.transform);
                uiDisplay.transform.SetAsLastSibling();
                uiDisplay.ActiveHazardMonoObj = h;
                h.activeUI = uiDisplay;
            }

            if (activeHazardsList.Count >= GameSystem.Instance.configManager.config.MaxActiveHazards)
            {
                HazardManagerMono.hazardSpawner.enabled = false;

                //Carriage.Instance.Stalled = true;
                var inActiveHazards = (from hazard in FindObjectsOfType<HazardMono>()
                                            where hazard.isActive == false
                                            select hazard);
                foreach (HazardMono iah in inActiveHazards)
                {
                    iah.PauseHazardsAdvancement();
                }
            }
            
        }

        public void Remove(HazardMono h)
        {
            if (activeHazardsList.Contains(h))
            {
                activeHazardsList.Remove(h);
                if (activeHazardsList.Count < GameSystem.Instance.configManager.config.MaxActiveHazards)
                {
                    HazardManagerMono.hazardSpawner.enabled = true;

                    //Carriage.Instance.Stalled = false;
                    var inActiveHazards = (from hazard in FindObjectsOfType<HazardMono>()
                                           where hazard.isActive == false
                                           select hazard);
                    foreach (HazardMono iah in inActiveHazards)
                    {
                        iah.ResumeHazardsAdvancement();
                    }

                }

            }
        }

        public HazardMono FindTop(Managers.HazardType t)
        {
            var h_list = (from hazard in activeHazardsList
                     where hazard.type == t
                     orderby hazard.gameObject.transform.GetSiblingIndex() ascending
                     select hazard);
            if (h_list.Count<HazardMono>() == 0) return null;
            else return h_list.First();
            
                
        }

    }
}