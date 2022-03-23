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
        public Vector2 inputVector = Vector2.zero;
        [SerializeField]
        Vector2 verticalVelocity = Vector2.zero;
        Vector2 horizontalVelocity = Vector2.zero;
        [SerializeField]
        GameObject feet;
        [SerializeField]
        float GroundDetection = 0.15f;

        float GRAVITY = 9.8f;
        public float GravityScale = 1f;
        float MaxFallSpeed = 10f;
        
        public TextMeshPro debugLabel;
        [Tooltip("Character's max horizontal velocity")]
        public float MaxVelocity = 0f;

        Vector2 currentVelocity = Vector2.zero; //used by damping function
        [SerializeField]
        [Tooltip("Controls how quickly the character reaches top speed.")]
            float SmoothTime = 1f;

        bool isGrounded = false;
        Rigidbody2D rb;

        public bool toJump = false;
        private CharacterController characterController;
        float movementFactor = -1f;
        private PlayerInput playerInput;

        public float JumpForce = 25f;

        public bool InsideCarriage {  get; set; }
        
        
        // Start is called before the first frame update
        void Start()
        {
            playerInput = GetComponent<PlayerInput>();
            rb = GetComponent<Rigidbody2D>();

        }
        // Update is called once per frame
        void FixedUpdate()
        {
            DetectGround();
            debugLabel.text = $"Grounded: {isGrounded}";

            if (toJump)
            {
                //Debug.Log("Jump performed");
                verticalVelocity = Vector2.up * JumpForce;
                toJump = false;
            }
            else if (!isGrounded)
            {
                verticalVelocity += GRAVITY * GravityScale * Vector2.down* Time.deltaTime;
                verticalVelocity.y = Mathf.Clamp(verticalVelocity.y, -MaxFallSpeed, MaxFallSpeed);
            }
            if (inputVector.x * inputVector.x > 0)
                horizontalVelocity = Vector2.SmoothDamp(horizontalVelocity, new Vector2(inputVector.x * MaxVelocity, 0), ref currentVelocity, SmoothTime);
            else
            {
                horizontalVelocity = Vector2.zero;
                currentVelocity = Vector2.zero;
            }
            Vector2 velocity = horizontalVelocity + verticalVelocity;
            //rb.MovePosition(transform.position + (Vector3)velocity*Time.deltaTime);
            rb.velocity = velocity;
            
        }


        public void OnPlayerMove(InputAction.CallbackContext context)
        {
            inputVector = context.ReadValue<Vector2>();
        }
        
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed) Debug.Log("Jump pressed");
            if (context.performed && isGrounded)
                toJump = true;
        }

        private void DetectGround()
        {
            if (feet == null) throw new System.ArgumentNullException($"{name}: feet not defined, cannot detect ground");
            RaycastHit2D hit;
            //Debug.DrawRay(feet.transform.position, ray.direction*GroundDetection, Color.red);
            

            LayerMask mask = ~LayerMask.GetMask("Player");
            hit = Physics2D.Raycast(feet.transform.position, Vector2.down,GroundDetection,mask);
            if (hit.collider != null)
            {
                verticalVelocity = Vector2.zero;
                isGrounded = true;
            }
            else isGrounded = false;

        }

    }
}