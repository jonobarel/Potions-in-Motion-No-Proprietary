using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ZeroPrep.MineBuddies
{
    public class ActionModule : ModuleBase
    {
        public Managers.HazardType hazardType;

        PowerModule Engine { get { return Carriage.Instance.Engine; } }
        [SerializeField]
        bool hasPower = false;
        [SerializeField]
        bool connected = false;
        [SerializeField]
        ParticleSystem particles;
        [SerializeField]
        [Range(1, 100)]
        int DamageToHazard;

        public string ActivatingPlayer;
        

        MoreMountains.CorgiEngine.ButtonActivated moduleActivator;

        public bool HasPower { get { return hasPower; }  set { hasPower = value; } }
        public bool IsConnected { get { return connected; } set { connected = value; } }
        
        public bool IsWorking { get {  return HasPower && IsConnected; } }

        // Start is called before the first frame update
        void Start()
        {
            moduleActivator = GetComponent<MoreMountains.CorgiEngine.ButtonActivated>();
        }

        public void Update()
        {
            if (Carriage.Instance.Engine.CurrentFuel < Helpers.Config.ModuleFuelConsumption)
            {
                moduleActivator.Activable = false;
            }
            else moduleActivator.Activable = true;
        }

        public void DamageHazard()
        {
            HazardMono hazardMono = GetTargetHazard();
            if (hazardMono == null || !Carriage.Instance.Engine.ModuleFuel() || !hazardMono.isActive) return;
            else
            {
                GameSystem.Instance.analytics.LogEvent(ActivatingPlayer, Analytics.LogAction.DamageHazard, hazardType, DamageToHazard, "player damaged hazard", hazardMono.id);
                hazardMono.MMHealth.Damage(DamageToHazard, gameObject, 0.5f, 0f, Vector3.zero);
                hazardMono.UpdateUIHealth();
            }
        }
        public void LogActivatingPlayer()
        {

        }
        HazardMono GetTargetHazard()
        {
            //find closest Hazard  of type Type to the carriage
            /*
             * 
             * var h_list = (from hazard in activeHazardsList
                     where hazard.type == t
                     orderby hazard.gameObject.transform.GetSiblingIndex() ascending
                     select hazard);
            if (h_list.Count<Hazard>() == 0) return null;
            */

            HazardMono[] hazardList = FindObjectsOfType<HazardMono>();
            var hazard = from h in hazardList
                          where h.type == hazardType
                          orderby h.SqrDistanceToCarriage() ascending
                          select h;
            if (hazard.Count() > 0 && hazard.First().isActiveAndEnabled)
            {
                Debug.DrawLine(transform.position, hazard.First().transform.position);
                return hazard.First();
                
            }
            return null;

        }
    }
}