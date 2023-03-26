using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace ZeroPrep.MineBuddies
{
    public class LerpDemo : MonoBehaviour
    {
        [SerializeField]
        Transform[] _positions = new Transform[3];
    
        private int current;
        private int target;
        [SerializeField]
        private float _durationSeconds = 5f;
        [SerializeField]
        private float _progress = 0f;

        
        void Update()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            if (current != target)
            {
                transform.position =
                    Vector3.Lerp(_positions[current].position, _positions[target].position, _progress / _durationSeconds);
                _progress += Time.deltaTime;
                if (_progress >= _durationSeconds)
                {
                    transform.position = _positions[target].position;
                    current = target;
                }
                
            }
        }
        
        public void OnInteract(InputValue value) {
            Debug.Log("switching states");
            target = (current + 1) % 3;
            _progress = 0f;
        }
        
    }
    
    
}