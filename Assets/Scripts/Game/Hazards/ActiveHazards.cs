using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace ZeroPrep.MineBuddies
{
    [RequireComponent(typeof(HazardManager))]
    public class ActiveHazards : MonoBehaviour
    {
        List<Hazard> activeHazardsList = new List<Hazard>();
        [SerializeField] 
        VerticalLayoutGroup activeHazardQueueUI;

        public HazardManager HazardManager {
            get {
                if (hazardManager == null) hazardManager = GetComponent<HazardManager>();
                if (hazardManager != null) return hazardManager;
                else Debug.LogError($"{name}: could not find HazardManager object");
                throw new System.Exception("HazardManager not found");
            } 
        }

        private HazardManager hazardManager;
        

        [SerializeField]
        ActiveHazardUI activeHazardUIPrefab;
        public int Count { get { return activeHazardsList.Count; } }

        public void Add(Hazard h)
        {
            if (!activeHazardsList.Contains(h))
            {
                activeHazardsList.Add(h);
                //Instantiate the UI for the new Hazard.
                ActiveHazardUI uiDisplay = Instantiate(activeHazardUIPrefab, activeHazardQueueUI.transform);
                uiDisplay.transform.SetAsLastSibling();
                uiDisplay.ActiveHazardObj = h;
                h.activeUI = uiDisplay;
            }

            if (activeHazardsList.Count >= GameSystem.Instance.configManager.config.MaxActiveHazards)
            {
                HazardManager.hazardSpawner.enabled = false;

                Carriage.Instance.Stalled = true;
                var inActiveHazards = (from hazard in FindObjectsOfType<Hazard>()
                                            where hazard.isActive == false
                                            select hazard);
                foreach (Hazard iah in inActiveHazards)
                {
                    iah.PauseHazardsAdvancement();
                }
            }
            
        }

        public void Remove(Hazard h)
        {
            if (activeHazardsList.Contains(h))
            {
                activeHazardsList.Remove(h);
                if (activeHazardsList.Count < GameSystem.Instance.configManager.config.MaxActiveHazards)
                {
                    HazardManager.hazardSpawner.enabled = true;

                    Carriage.Instance.Stalled = false;
                    var inActiveHazards = (from hazard in FindObjectsOfType<Hazard>()
                                           where hazard.isActive == false
                                           select hazard);
                    foreach (Hazard iah in inActiveHazards)
                    {
                        iah.ResumeHazardsAdvancement();
                    }

                }

            }
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