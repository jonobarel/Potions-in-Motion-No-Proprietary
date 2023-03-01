using TMPro;
using UnityEngine;


namespace ZeroPrep.MineBuddies
{


    public class VectorDisplay : MonoBehaviour
    {
        [SerializeField] private RotationInput _rotationInput;
        private TextMeshProUGUI _tmp;

        void Awake()
        {
            _tmp = GetComponent<TextMeshProUGUI>();
        }
        public void Update()
        {
            _tmp.text = _rotationInput.DirVector.ToString();
        }
        
    }
}