using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace com.ZeroPrepGames.TrollTruckerTales
{
    public class ActionModule : ModuleBase
    {
        public GameManager.HazardType hazardType;

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

        // Update is called once per frame
        
        public override void Interact(bool isStart, Dwarf player)
        {
            
            if (isStart)
            {//begin interaction
                Debug.Log($"{name} - begin activation by {player.name}");
                //Engine.Connect(this);
            }
            else
            {
                Debug.Log($"{name} - stop");
                //Engine.Disconnect(this);
            }
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
            Hazard hazard = GetTargetHazard();
            if (hazard == null || !Carriage.Instance.Engine.ModuleFuel() || !hazard.isActive) return;
            else
            {
                GameSystem.Instance.analytics.LogEvent(ActivatingPlayer, Analytics.LogAction.DamageHazard, hazardType, DamageToHazard, "player damaged hazard");
                hazard.MMHealth.Damage(DamageToHazard, gameObject, 0.5f, 0f, Vector3.zero);
                hazard.UpdateUIHealth();
            }
        }
        public void LogActivatingPlayer()
        {

        }
        Hazard GetTargetHazard()
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

            Hazard[] hazardList = FindObjectsOfType<Hazard>();
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