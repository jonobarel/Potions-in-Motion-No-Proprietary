using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    public class BackgroundScrolling : MonoBehaviour
    {
        [SerializeField]
        Transform[] layers;
        float[] layerParallaxFactors;
        SpriteRenderer[] spriteRenderers;
        private EngineSpeed _engineSpeed;
        
        [Inject]
        void Init(EngineSpeed engineSpeed)
        {
            _engineSpeed = engineSpeed;
        }
        
        void Start()
        {
            //DontDestroyOnLoad(gameObject);
            layers = (from t in GetComponentsInChildren<Transform>()
                      where t != transform
                      orderby t.GetSiblingIndex() ascending
                      select t).ToArray();
            spriteRenderers = (from layer in layers select layer.GetComponent<SpriteRenderer>()).ToArray();

            ConfigManager cfg = (GameSystem.Instance)? GameSystem.ConfigManager : FindObjectOfType<ConfigManager>();
            layerParallaxFactors = cfg.config.layersParallaxFactors;
            if (layers.Length > layerParallaxFactors.Length)
            {
                throw new System.ArgumentOutOfRangeException($"{name}: not all scrolling layers have a parallax factor in the config file");
            }
        }

        // Update is called once per frame
        void Update()
        {
            float currentSpeed;
            currentSpeed = _engineSpeed.CurrentSpeed();
            for (int i = 0; i < layers.Length; i++) {
                float factor = layerParallaxFactors[i];
                float delta = factor*currentSpeed*Time.deltaTime;
                layers[i].position+=new Vector3(-delta, 0,0);
                spriteRenderers[i].size += new Vector2 (delta, 0);
            }
        }
    }
}