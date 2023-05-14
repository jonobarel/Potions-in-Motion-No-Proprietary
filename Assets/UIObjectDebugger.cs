using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIObjectDebugger : MonoBehaviour
{
    [SerializeField][TextArea(4,10)] string notes = "This script is used to debug UI objects. It is not intended for use in production.";
    private RectTransform rectTransform;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void OnGUI()
    {
        Vector3 position = transform.position;
        Vector2 anchoredPosition = rectTransform.anchoredPosition;
        Vector2 viewPortSize = new Vector2(Screen.width, Screen.height);
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(position);
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(position);

        notes = $"Anchored position: {anchoredPosition}\n"+ 
                $"Screenpoint: {screenPoint}\n" +
                $"Viewport position: {viewportPoint}" +
                $"ViewportSize: {viewPortSize}\n";
    }
    
    
}
