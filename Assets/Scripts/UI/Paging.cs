using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paging : MonoBehaviour
{

    public GameObject NextButton;
    public GameObject PrevButton;
    public string TitleScene;
    public Transform[] images;
    public Transform CenterPosition;
    public int position = 0;

    public float SwoopSize;
    public float SwoopDuration = 0.75f;

    public void Start()
    {
        SwoopSize = 5000;
        for (int i = 1; i < images.Length; i++)
        {
            images[i].position = CenterPosition.position + new Vector3(SwoopSize, 0, 0);
        }
    }

    public void Next()
    {
        if (position < images.Length)
        {
            Swoop(images[position], -SwoopSize);
            position++;
        }
        if (position < images.Length)
        {
            Swoop(images[position], -SwoopSize);
        }
        else
            MoreMountains.Tools.MMSceneLoadingManager.LoadScene(TitleScene);
    }

    public void Prev()
    {
        if (position >= 0)
        {
            Swoop(images[position], SwoopSize);
            position--;
        }
        if (position >= 0)
        {
            Swoop(images[position], SwoopSize);
        }
        else MoreMountains.Tools.MMSceneLoadingManager.LoadScene(TitleScene);
    }
    public void Swoop(Transform obj, float offset)
    {
        obj.position += new Vector3( offset,0,0);
       /* float startX = obj.position.x;
        float targetX = startX + offset;

        this.CreateAnimationRoutine(
            SwoopDuration,
            delegate (float progress)
            {
                float easedProgress = Easing.easeInOutSine(0, 1, progress);
                float pos = Mathf.Lerp(startX, targetX, easedProgress);
                transform.position += new Vector3(pos, 0, 0);
            }
            );*/

    }

    
}
