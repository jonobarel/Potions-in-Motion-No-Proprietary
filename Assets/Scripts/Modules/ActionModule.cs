using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.baltamstudios.minebuddies
{
    public class ActionModule : ModuleBase
    {
        public GameManager.HazardType hazardType;

        PowerModule Engine { get { return Carriage.Instance.Engine; } }
        bool hasPower = false;
        bool connected = false;
        [SerializeField]
        ParticleSystem particles;

        public bool HasPower { get { return hasPower; }  set { hasPower = value; } }
        public bool IsConnected { get { return connected; } }


        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            var em = particles.emission;
            if (connected && hasPower)
            {
                Debug.Log($"{name} - Activating module");
                em.enabled = true;
            }
            else em.enabled = false;
        }
        
        public override void Interact(bool isStart, Dwarf player)
        {
            
            if (isStart)
            {//begin interaction
                Debug.Log($"{name} - begin activation by {player.name}");
                Engine.Connect(this);
            }
            else
            {
                Debug.Log($"{name} - stop");
                Engine.Disconnect(this);
            }
        }


    }
}