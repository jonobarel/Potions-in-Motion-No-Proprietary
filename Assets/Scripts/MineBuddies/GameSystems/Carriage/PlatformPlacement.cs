using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ModestTree;
using MoreMountains.Tools;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


namespace ZeroPrep.MineBuddies
{
    public class PlatformPlacement : ObjectContainer<Transform>
    {
        [SerializeField]
        GameObject platformPrefab;

        
        
        [SerializeField]
        int platformCount;

        [SerializeField]
        float jumpHeight = 2f;

        [SerializeField] private float minXdistance = 4.8f;
        [SerializeField] private float minYdistance = 1.5f;

        [SerializeField] private Grid truckGrid;
        [SerializeField] private int maxPlacementAttempts = 100;
        
        [SerializeField] private HashSet<Vector3Int> _platformPositions = new HashSet<Vector3Int>(); 
        
        public Grid TruckGrid
        {
            get
            {
                if (truckGrid == null)
                {
                    truckGrid = GetComponentInChildren<Grid>();
                }

                return truckGrid;
            }

            set => truckGrid = value;
        }
        
        [SerializeField]
        private Bounds levelBounds;

        public Bounds ScaledBounds => new Bounds()
        {
            min = Vector3.Scale(levelBounds.min, transform.localScale),
            max = Vector3.Scale(levelBounds.max, transform.localScale)
        };
        
        public override void PlaceObjects(int instanceCount, bool clearContents = false)
        {
            base.PlaceObjects(instanceCount, clearContents);

            Bounds scaledBounds = ScaledBounds;

            foreach (var platform in Contents)
            {
                Vector2 pos = TruckGrid.CellToWorld(GetRandomGridPosition(scaledBounds));
                platform.position = pos;
            }
        }

        public override void PlaceObjects()
        {
            PlaceObjects(platformCount, true);
        }

        private Vector3Int GetRandomGridPosition(Bounds placementBounds)
        {

            if (_platformPositions.Count >= MaxPlatformPositions())
            {
                throw new ArgumentOutOfRangeException("Too many platforms cannot place more");
                
            }
            Vector3Int min = TruckGrid.WorldToCell((Vector3)placementBounds.min);
            Vector3Int max = TruckGrid.WorldToCell((Vector3)placementBounds.max);
            int attempts = 0;
            Vector3Int randomPos;
            do
            {
                randomPos = new Vector3Int(Random.Range(min.x, max.x)+1, Random.Range(min.y, max.y)+1, 0);
            } while (_platformPositions.Contains(randomPos) && attempts++ <= maxPlacementAttempts);

            _platformPositions.Add(randomPos);
            return randomPos;
        }

        private int MaxPlatformPositions()
        {
            Bounds scaledBounds = ScaledBounds;
            Vector3Int min = TruckGrid.WorldToCell((Vector3) scaledBounds.min);
            Vector3Int max = TruckGrid.WorldToCell((Vector3) scaledBounds.max);
            return (max.x - min.x) * (max.y - min.y);
        }

        public override void ClearContents()
        {
            base.ClearContents();
            _platformPositions.Clear();
        }
    }
}