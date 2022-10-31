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
        _sliders = new Dictionary<HazardBase, GameObject>();
        HazardBase.Spawn += OnSpawn;
        HazardBase.Clear += OnClear;
        HazardBase.Expire += OnExpire;
    }

    void OnDestroy()
    {
        HazardBase.Spawn -= OnSpawn;
        HazardBase.Clear -= OnClear;
        HazardBase.Expire -= OnExpire;
    }
    private void OnSpawn(HazardBase h)
    {
        AddHazardToTimeline(h);
    }
    private void AddHazardToTimeline(HazardBase h)
    {
        //Instantiate the UI element and
        //add it to the timeline

        Transform sliderContainer = Slidercontainer;
        Slider prefab = ProgressSliderPrefab;

        Slider positionSlider = Object.Instantiate(prefab, sliderContainer);
        _sliders.Add(h, positionSlider.gameObject);
        positionSlider.GetComponent<HazardSliderDisplay>().Init(h);

        /*positionSlider = Instantiate(hazardManager.PositionSliderPrefab, hazardManager.HazardDistanceSliderContainer);
        positionSlider.GetComponent<HazardSliderDisplay>().HazardMono = this;
        */
    }

    private void OnExpire(HazardBase h)
    {
        HazardExpired(h);
    }
    private void HazardExpired(HazardBase h)
    {
        HardRemoveSliderFromTimeline(h);
    }

    private void OnClear(HazardBase h)
    {
        HazardCleared(h);
    }
    private void HazardCleared(HazardBase h)
    {
        HardRemoveSliderFromTimeline(h);
    }

    private void HardRemoveSliderFromTimeline(HazardBase h)
    {
        GameObject positionSlider;
        if (_sliders.Remove(h, out positionSlider) && positionSlider)
        {
            GameObject.Destroy(positionSlider);
        }
    }
    

}
