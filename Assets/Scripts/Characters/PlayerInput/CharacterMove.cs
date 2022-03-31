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
        #region constants
        [SerializeField]
        float GroundDetection = 0.15f;
        float GRAVITY = 9.8f;
        public float GravityScale = 1f;
        float MaxFallSpeed = 10f;
        [Tooltip("Character's max horizontal velocity")]
        public float MaxVelocity = 0f;
        [SerializeField]
        [Tooltip("Controls how quickly the character reaches top speed.")]
        float SmoothTime = 1f;
        public float JumpForce = 25f;
        [SerializeField]
        float ClimbSpeed = 25f;

        float LadderWalkThresholdSqr = 0.85f;
        float LadderClimbThresholdSqr = 0.2f;
        #endregion constants


        #region variables
        public Vector2 inputVector = Vector2.zero;
        [SerializeField]
        Vector2 verticalVelocity = Vector2.zero;
        Vector2 horizontalVelocity = Vector2.zero;
        [SerializeField]
        Vector2 currentVelocity = Vector2.zero; //used by damping function

        [SerializeField]
        bool isGrounded = false;
        bool toJump = false;

        bool isClimbing = false;
        GameObject ladder = null;
        [SerializeField]
        bool isNearLadder;
        
        #endregion variables

        #region Components
        [SerializeField]
        GameObject feet;
        public TextMeshPro debugLabel;
        Rigidbody2D rb;
        private PlayerInput playerInput;

        #endregion

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

            if (!isClimbing)
            {
                if (isNearLadder && (inputVector.y*inputVector.y >= LadderClimbThresholdSqr))
                {
                    isClimbing = true;
                    rb.MovePosition(new Vector2(ladder.transform.position.x, transform.position.y));
                }
            }

            if (isClimbing)
            {
                //rb.MovePosition(new Vector2(ladder.transform.position.x, transform.position.y)); //lock the X position to the ladder's
                if (inputVector.x * inputVector.x < LadderWalkThresholdSqr) inputVector.x = 0f; //ignore horizontal input
                verticalVelocity = inputVector * ClimbSpeed;
            }
            else if (!isGrounded)
            {
                verticalVelocity += GRAVITY * GravityScale * Vector2.down * Time.deltaTime;
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

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ladder"))
            {
                ladder = other.gameObject;
                isNearLadder = true;
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Ladder"))
            {
                ladder = null;
                isNearLadder = false;
                isClimbing = false;
            }
        }

    }
}