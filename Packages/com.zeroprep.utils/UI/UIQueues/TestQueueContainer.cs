using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZeroPrep.UI;

public class TestQueueContainer : MonoBehaviour
{
    public RectTransform itemPrefab;
    public QueueContainer container;
    public void OnClick()
    {
        RectTransform newItem = Instantiate(itemPrefab);
        container.AddObjectToQueue(newItem);
    }
    
}
