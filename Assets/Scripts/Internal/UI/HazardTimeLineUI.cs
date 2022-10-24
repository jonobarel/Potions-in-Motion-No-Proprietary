using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZeroPrep.MineBuddies;

public class HazardTimeLineUI : MonoBehaviour
{
    /// <summary>
    /// This is the MonoBehaviour script attached to the HazardTimeline UI.
    /// It handles creating the Slider manager which instantiating the Hazard sliders
    /// and providing access to scene elements.
    /// </summary>
    // Start is called before the first frame update
    public GameObject hazardProgressDisplayPrefab;

    private HazardSliderUIManager _hazardSliderUIManager;
    void Start()
    {
        _hazardSliderUIManager = new HazardSliderUIManager(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
