using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ClearFeedbackAnimation : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private Image[] _images;
    void Start()
    {
        _images = target.GetComponentsInChildren<Image>();
    }
    
    public void Play()
    {
        foreach (var image in _images)
        {
            image.DOColor(Color.green, 0.5f);
        }
    }

}
