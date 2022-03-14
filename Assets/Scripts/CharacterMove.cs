using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace com.baltamstudios.minebuddies
{
    public class CharacterMove : MonoBehaviour
    {

        public Vector2 inputVector = Vector2.zero;
        public bool toJump = false;
        private CharacterController characterController;
        float movementFactor = -1f;
        public float characterSpeed = 5.0f;
        private PlayerInput playerInput;
        
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
            if (inputVector != Vector2.zero)
            {
                Vector3 movementVector;
                if (!InsideCarriage)
                {
                    movementVector = ConvertToGameSpace(inputVector);
                }
                else
                    movementVector = (Vector3)inputVector;
                
                characterController.Move(movementVector * characterSpeed * Time.deltaTime);
            }

            if (toJump)
            {
                Debug.Log("Implement Jump");
                toJump = false;
                throw new System.NotImplementedException("Implement jump function");
            }
        }


        public void OnPlayerMove(InputAction.CallbackContext context)
        {
            inputVector = context.ReadValue<Vector2>();
        }
        
        public void OnJump(InputAction.CallbackContext context)
        {
            toJump = context.performed;
        }

        private Vector3 ConvertToGameSpace(Vector2 inputVector)
        {
            Vector3 v = new Vector3(inputVector.x, 0f, inputVector.y * MovementFactor);
            return v;
        }

    }
}