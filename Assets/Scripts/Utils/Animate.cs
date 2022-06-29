using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.ZeroPrepGames.TrollTruckerTales
{
    public class Animate : MonoBehaviour
    {
        float animationDuration = 1f;
        Coroutine fadeRoutine;
        Coroutine flattenRoutine;
        Image[] images;

        public void Start()
        {
            images = GetComponentsInChildren<Image>();
        }

        public void DoFadeAnimation()
        {
            Color startColor = Color.white;
            Color targetColor = Color.clear;
            fadeRoutine = this.CreateAnimationRoutine(
                animationDuration,
                delegate (float progress)
                {
                    float easedProgress = Easing.easeInOutSine(0, 1, progress);
                    Color col = Color.Lerp(startColor, targetColor, easedProgress);
                    foreach (Image i in images)
                    {
                        i.color = col;
                    }
                }
            );
        }

        public void DoShrinkAnimation()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            LayoutElement layoutElement = GetComponent<LayoutElement>();
            float startHeight = rectTransform.localScale.y;
            float targetHeight = 0f;
            flattenRoutine = this.CreateAnimationRoutine(
                animationDuration,
                delegate (float progress)
                {
                    float easedProgress = Easing.easeInOutSine(0, 1, progress);
                    float height = Mathf.Lerp(startHeight, targetHeight, easedProgress);
                    Vector3 scale = rectTransform.localScale;
                    rectTransform.localScale = new Vector3(scale.x, height, scale.z);
                    layoutElement.preferredHeight = rectTransform.localScale.y;
                }
                );

        }

    }
}