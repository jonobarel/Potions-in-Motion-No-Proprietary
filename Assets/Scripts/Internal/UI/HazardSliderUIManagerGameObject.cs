using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZeroPrep.MineBuddies;
using UnityEngine.UI;


public class HazardSliderUIManagerGameObject : MonoBehaviour
{
    /// <summary>
    /// This is the MonoBehaviour script attached to the HazardTimeline UI.
    /// It handles creating the Slider manager which instantiating the Hazard sliders
    /// and providing access to scene elements.
    /// </summary>
    // Start is called before the first frame update
    
    [SerializeField]
    private Slider _hazardProgressDisplayPrefab;
    public Slider ProgressSliderPrefab => _hazardProgressDisplayPrefab;

    [SerializeField]
    private Transform _hazardSliderContainer;
    public Transform Slidercontainer => _hazardSliderContainer;     
    //private HazardSliderUIManager _hazardSliderUIManager;
    private Dictionary<HazardBase, GameObject> _sliders;
    
    void Start()
    {
        HazardBase.OnSpawn += AddHazardToTimeline;
        HazardBase.OnClear += HazardCleared;
        HazardBase.OnExpire += HazardExpired;
    }

    private void AddHazardToTimeline(HazardBase h)
    {
        //Instantiate the UI element and
        //add it to the timeline

        Transform sliderContainer = Slidercontainer;
        Slider prefab = ProgressSliderPrefab;

        Slider positionSlider = Object.Instantiate(prefab, sliderContainer);
            
        /*positionSlider = Instantiate(hazardManager.PositionSliderPrefab, hazardManager.HazardDistanceSliderContainer);
        positionSlider.GetComponent<HazardSliderDisplay>().HazardMono = this;
        */
    }

    private void HazardExpired(HazardBase h)
    {
        throw new System.NotImplementedException();
    }

    private void HazardCleared(HazardBase h)
    {
        throw new System.NotImplementedException();
    }
    

}
