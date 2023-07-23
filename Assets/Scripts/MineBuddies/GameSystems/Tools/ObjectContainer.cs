using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using MoreMountains.Tools;
using UnityEditor;

namespace ZeroPrep.MineBuddies
{
    /// <summary>
    /// Used by instantiation classes like "Module placement" and "Platform placement" to store and place objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ObjectContainer<T> : MonoBehaviour
        where T : Component
    {
        private HashSet<T> _contents = new HashSet<T>();

        /// <summary>
        /// parent transform for all objects in the container
        /// </summary>
        [SerializeField] protected Transform contentsParent;

        /// <summary>
        /// When instantiating contents, this is the prefab that will be used.
        /// </summary>
        [SerializeField] protected T objectPrefab;

        public T Prefab => objectPrefab;
        

        /// <summary>
        /// Accessor for contained objects
        /// </summary>
        public HashSet<T> Contents
        {
            get
            {
                if (_contents == null || _contents.IsEmpty())
                {
                    _contents = new HashSet<T>(GetComponentsInChildren<T>().Where(t => t.transform.parent == contentsParent));
                }
                
                return _contents;
            }
        }

        

        /// <summary>
        /// Clears all contents from the object container
        /// </summary>
        public virtual void ClearContents()
        {
            foreach (T obj in Contents)
            {
                DestroyItem(obj);
            }
            
            _contents.Clear();
        }


        private void DestroyItem(T obj)
        {
            if (obj != null)
            {
#if UNITY_EDITOR
                DestroyImmediate(obj.gameObject);
#else
                Destroy(obj.gameObject);
#endif
            }
        }
        
        
        /// <summary>
        /// Generates prefab instances of the base object and places them in the container
        /// </summary>
        /// <param name="i">number of instances to create</param>
        /// <param name="clearContents">if <c>true</c>, will destroy all existing instances.</param>
        public virtual void PlaceObjects(int i, bool clearContents = false )
        {
            if (clearContents)
            {
                ClearContents();
            }
            
            for (int j = 0; j < i; j++)
            {
                T newObject = PrefabUtility.InstantiatePrefab(objectPrefab, contentsParent) as T;
                _contents.Add(newObject);
            }
        }

        public abstract void PlaceObjects();

        public T[] GetRandomContent(int i = 1)
        {
            return Contents.ToArray().MMShuffle().Take(i).ToArray();
        }
    }
}