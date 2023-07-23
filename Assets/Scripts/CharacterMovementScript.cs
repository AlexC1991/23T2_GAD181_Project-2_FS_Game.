using System;
using UnityEngine;

namespace AlexzanderCowell
{
    public class CharacterMovementScript : MonoBehaviour
    {
        [Header("Character Movement")]
        private float _mouseXposition,
            _moveHorizontal,
            _moveVertical,
            _mouseYposition;

        [HideInInspector] public float mouseSensitivityY,
            mouseSensitivityX;
        [SerializeField] private float runSpeed;
        private CharacterController controller;
        private Vector3 _moveDirection;
        private bool _playerIsJumping;
        private float jumpHeight, characterGravity;
        [SerializeField] private float downValue, upValue;
        private Transform _cameraTransform;

        // Declaration of game objects for use in the Equipment script
        [Header("Equipment Settings")]
        public static bool holdingEquipment = false;
        [SerializeField] public GameObject eqiupmentHoldPosition;
        [SerializeField] public GameObject equipmentShovel;
        [SerializeField] public GameObject equipmentHammer;
        [SerializeField] public GameObject equipmentAxe;
        [SerializeField] public GameObject equipmentSpade;
        [SerializeField] public GameObject equipmentNet;

        //Declaration of the Audio source for SFX
        [Header("SFX Based Settings")]
        [SerializeField] private AudioSource characterSFXSource;
        [SerializeField] private AudioClip footstepSFX;
        private bool footstepAudioPlaying = false;
        public static bool insideTreeCircle;

        // Declaration of the InGameTutorial object
        [SerializeField] private GameObject gameTut;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Tree"))
            {
                insideTreeCircle = true;
            }
        }

        private void Start()
        {
            equipmentNet.GetComponent<Animator>().enabled = false;
            _cameraTransform = Camera.main.transform;
            controller = GetComponent<CharacterController>();
            jumpHeight = 2;
            mouseSensitivityY = 0.7f;
            mouseSensitivityX = 1;
            runSpeed = 9f;
            _playerIsJumping = false;
            characterGravity = 25;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            _mouseXposition += mouseSensitivityX * Input.GetAxis("Mouse X"); // grabs the mouse X axis every frame for the rotation movement.
            _mouseYposition -= mouseSensitivityY * Input.GetAxis("Mouse Y"); // grabs the mouse Y axis every frame for the rotation movement.
            _moveHorizontal = Input.GetAxis("Horizontal"); // Gets the horizontal movement of the character.
            _moveVertical = Input.GetAxis("Vertical"); // Gets the vertical movement of the character.

            JumpMovement(); // Controls the jump movement of the character.

            transform.rotation = Quaternion.Euler(0f, _mouseXposition, 0f);
            _cameraTransform.rotation = Quaternion.Euler(_mouseYposition, _mouseXposition, 0f);
            _mouseYposition = Mathf.Clamp(_mouseYposition, downValue, upValue);
            Vector3 movement = new Vector3(_moveHorizontal, 0f, _moveVertical); // Allows the character to move forwards and backwards & left & right.
            movement = transform.TransformDirection(movement) * runSpeed; // Gives the character movement speed.
            controller.Move((movement + _moveDirection) * Time.deltaTime); // Gets all the movement variables and moves the character.

            FootstepSFX();
            
            if (InGameTutorial.firstMove == true)
            {
                // Checks if first time then if it is progresses tutorial stage
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                {
                    InGameTutorial.tutorialAudioSource.Stop();
                    MainMenu.tutorialStage++;
                    InGameTutorial.firstMove = false;
                   
                    gameTut.GetComponent<InGameTutorial>().RunTutorial();
                }
            }
        }

        private void JumpMovement()
        {
            if (Input.GetKeyDown(KeyCode.Space) && (controller.isGrounded)) // If player hits the space bar and the character is touching the ground it will allow the character to jump.
            {
                _moveDirection.y = Mathf.Sqrt(2f * jumpHeight * characterGravity);
                _playerIsJumping = true;
            }

            if (controller.isGrounded) return;
            _moveDirection.y -= characterGravity * Time.deltaTime;
            _playerIsJumping = false;
        }

        // Method that plays the footstep sfx when moving
        private void FootstepSFX()
        {
            if ((_moveVertical != 0f || _moveHorizontal != 0f) && footstepAudioPlaying == false)
            {
                characterSFXSource.PlayOneShot(footstepSFX);
                footstepAudioPlaying = true;
            }
            else
            {
                characterSFXSource.Stop();
                footstepAudioPlaying = false;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Tree"))
            {
                insideTreeCircle = false;
            }
        }
    }
}
