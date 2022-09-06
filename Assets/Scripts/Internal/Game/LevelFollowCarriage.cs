using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZeroPrep.MineBuddies
{
    public class LevelFollowCarriage : MonoBehaviour
    {
        Transform carriageTransform;
        Vector2 LevelBoundsOffset;
        Vector2 LevelManagerOffset;
        MoreMountains.CorgiEngine.MultiplayerLevelManager levelManager;

        // Start is called before the first frame update
        void Start()
        {
            try
            {
                levelManager = GetComponent<MoreMountains.CorgiEngine.MultiplayerLevelManager>();
                carriageTransform = Carriage.Instance.transform;
                if (levelManager == null)
                {
                    Debug.Log("Level Manager is null");
                    return;
                }
                if (carriageTransform == null)
                {
                    Debug.Log("CarriageTransform is null");
                    return;
                }
            }
            catch (Exception e)
            {
                Debug.Log("Tried to init LevelManager and carriagetransform");
                Debug.LogException(e);
            }

            try
            {
                LevelManagerOffset = transform.position - carriageTransform.position;
                Debug.Log($"LevelManagerOffset: {LevelManagerOffset}");
                LevelBoundsOffset = (Vector2)(levelManager.LevelBounds.center - transform.position);
                Debug.Log($"LevelBoundsOffset: {LevelBoundsOffset}");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = (Vector2)carriageTransform.position + LevelManagerOffset;
            levelManager.LevelBounds.center = (Vector2)transform.position + LevelBoundsOffset;
        }
    }
}