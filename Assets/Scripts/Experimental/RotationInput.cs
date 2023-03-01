using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


public class RotationInput : MonoBehaviour
{
    private PlayerInput _playerInput;
    
    private Vector2 _dir;
    private Vector2 _previousDir;
    public float DeltaAngles { private set; get; }
    [FormerlySerializedAs("target")] [SerializeField] private GameObject matchRotationTarget;
    [SerializeField] private GameObject progressiveRotationTarget;
    [SerializeField] private float maxRotationRate = 15f;
    [SerializeField] private float progressiveRotationFactor = 5f;
    
    
    public Vector2 DirVector => _dir;
    void Start()
    {
        _playerInput=GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        _previousDir = _dir;
        _dir = _playerInput.actions["Rotation"].ReadValue<Vector2>();
        matchRotationTarget.transform.rotation = Quaternion.LookRotation(Vector3.forward, _dir);

        DeltaAngles = Quaternion.FromToRotation(_previousDir, _dir).eulerAngles.z;
        if (DeltaAngles > 180)
        {
            DeltaAngles -= 360;
        }

        float rotationAngle = Mathf.Clamp(DeltaAngles, -maxRotationRate, maxRotationRate);
        progressiveRotationTarget.transform.Rotate(Vector3.forward, rotationAngle*progressiveRotationFactor);


    }

    
}
