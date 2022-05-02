using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace com.baltamstudios.minebuddies
{
    public class BackgroundScrolling : MonoBehaviour
    {
        [SerializeField]
        Transform[] layers;
        float[] layerParallaxFactors;

        void Start()
        {
            DontDestroyOnLoad(gameObject);
            layers = (from t in GetComponentsInChildren<Transform>()
                      where t != transform
                      orderby t.GetSiblingIndex() ascending
                      select t).ToArray();

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
            if (Carriage.Instance != null)
                currentSpeed = Carriage.Instance.CarriageMovement.CurrentSpeed;
            else currentSpeed = 10f;
            for (int i = 0; i < layers.Length; i++) {
                float factor = layerParallaxFactors[i];
                layers[i].position+=new Vector3(-factor*currentSpeed*Time.deltaTime, 0,0);
            }
        }
    }
}