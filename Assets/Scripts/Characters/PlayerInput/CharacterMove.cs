using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

namespace com.baltamstudios.minebuddies
{
    public class CharacterMove : MonoBehaviour
    {
        public GameObject feet;
        public Vector2 inputVector = Vector2.zero;
        private Vector3 verticalVelocity = Vector3.zero;
        private float GRAVITY = 9.8f;
        public float GravityScale = 1f;
        public TextMeshPro debugLabel;
        public float MaxVelocity = 0f;

        [SerializeField]
        float GroundDetection = 0.15f;
        Vector3 movementVector = Vector3.zero;
        bool isGrounded = false;

        public bool toJump = false;
        private CharacterController characterController;
        float movementFactor = -1f;
        public float characterSpeed = 5.0f;
        private PlayerInput playerInput;

        public float JumpForce = 25f;

        public bool InsideCarriage {  get; set; }
        
        float MovementFactor
        {
            get
            {
                if (movementFactor < 0f)
                    movementFactor = FindObjectOfType<MiningCamera>().cameraAngleFactor;
                return movementFactor;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            characterController = GetComponent<CharacterController>();
            playerInput = GetComponent<PlayerInput>();

        }
        // Update is called once per frame
        void Update()
        {
            DetectGround();

            debugLabel.text = $"Grounded: {isGrounded}";

            if (toJump)
            {
                //Debug.Log("Jump performed");
                verticalVelocity = Vector3.up * JumpForce;
                toJump = false;
            }

            

            //navigation inside and outside the carriage
            if (!InsideCarriage)
            { //outside - convert the vertical input to Z axis
                movementVector = ConvertToGameSpace(inputVector);
            }
            else
                movementVector = (Vector3)inputVector;
            characterController.Move((movementVector * characterSpeed + verticalVelocity) * Time.deltaTime);
            verticalVelocity += Vector3.down * GRAVITY * GravityScale * Time.deltaTime;
        }


        public void OnPlayerMove(InputAction.CallbackContext context)
        {
            inputVector = context.ReadValue<Vector2>();
        }
        
        public void OnJump(InputAction.CallbackContext context)
        {
            //Debug.Log("Jump pressed");
            if (context.performed && isGrounded)
                toJump = true;
        }

        private void DetectGround()
        {
            if (feet == null) throw new System.ArgumentNullException($"{name}: feet not defined, cannot detect ground");
            Ray ray = new Ray(feet.transform.position, Vector3.down);
            //Debug.DrawRay(feet.transform.position, ray.direction*GroundDetection, Color.red);
            RaycastHit hitData;

            LayerMask mask = ~LayerMask.GetMask("Player");

            if (Physics.Raycast(ray,out hitData, GroundDetection, mask))
            {
                verticalVelocity = Vector3.zero;
                isGrounded = true;
            }
            else isGrounded = false;

        }

        private Vector3 ConvertToGameSpace(Vector2 inputVector)
        {
            Vector3 v = new Vector3(inputVector.x, 0f, inputVector.y * MovementFactor);
            return v;
        }

    }
}