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

        [SerializeField] LayerMask groundLayers;
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
            //debugLabel.text = $"Grounded: {isGrounded}";

            if (toJump && isGrounded && !isClimbing)
            {
                //Debug.Log("Jump performed");
                rb.AddForce(Vector2.up*JumpForce, ForceMode2D.Impulse);
                toJump = false;
            }

            if (!isClimbing)
            {
                //if standing near a ladder and pressing up or down - start climbing.
                if (isNearLadder && (inputVector.y*inputVector.y >= LadderClimbThresholdSqr))
                {
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    isClimbing = true;
                    rb.MovePosition(new Vector2(ladder.transform.position.x, transform.position.y)); //snap the character to the ladder X position
                }
            }

            if (isClimbing)
            {
                if (inputVector.x * inputVector.x < LadderWalkThresholdSqr) inputVector.x = 0f; //ignore horizontal input
                verticalVelocity = inputVector * ClimbSpeed;
                rb.MovePosition((Vector3)(new Vector2(ladder.transform.position.x, transform.position.y) + verticalVelocity * Time.deltaTime));
            }

            else if (inputVector.x * inputVector.x > 0) // has horizontal input
            {
                horizontalVelocity = Vector2.SmoothDamp(horizontalVelocity, new Vector2(inputVector.x * MaxVelocity, 0), ref currentVelocity, SmoothTime);
                
            }
            else horizontalVelocity = Vector2.zero;

            rb.velocity = new Vector2(horizontalVelocity.x, rb.velocity.y);

        }


        public void OnPlayerMove(InputAction.CallbackContext context)
        {
            inputVector = context.ReadValue<Vector2>();
        }
        
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed && isGrounded)
                toJump = true;
        }

        private void DetectGround()
        {
            if (feet == null) throw new System.ArgumentNullException($"{name}: feet not defined, cannot detect ground");
            RaycastHit2D hit;
            //Debug.DrawRay(feet.transform.position, ray.direction*GroundDetection, Color.red);
            

            hit = Physics2D.Raycast(feet.transform.position, Vector2.down,GroundDetection,groundLayers);
            if (hit.collider != null)
            {
                //verticalVelocity = Vector2.zero;
                isGrounded = true;

                //also stop climbing.
                isClimbing = false;
                rb.bodyType = RigidbodyType2D.Dynamic;
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
            if (collision.CompareTag("Ladder") && ladder != null && ladder == collision.gameObject)
            {
                ladder = null;
                isNearLadder = false;
                isClimbing = false;
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }

    }
}