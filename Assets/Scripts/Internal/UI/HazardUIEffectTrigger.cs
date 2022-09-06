using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroPrep.MineBuddies
{
    public class HazardUIEffectTrigger : MonoBehaviour
    {
        [SerializeField]

        public int FeedbackLevel = 1;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Hazard"))
            {
                Debug.Log($"{ collision.gameObject.name} entered {name}");
                var h = collision.gameObject.GetComponent<Hazard>();
                if (FeedbackLevel > h.activeUI.feedbacks.Length)
                {
                    Debug.Log($"{name}: trying to trigger feedback effects on {h.activeUI.name}, but feedback level is out of bounds on the UI");
                    throw new System.ArgumentOutOfRangeException($"{name}: Trying to access missing HazardUI feedbacks");
                }
                var feedbacks = h.activeUI.feedbacks;
                feedbacks[FeedbackLevel].Play(Vector3.zero);
            }
        }
    }
}