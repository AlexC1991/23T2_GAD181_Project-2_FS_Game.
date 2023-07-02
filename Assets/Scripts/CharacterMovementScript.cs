using UnityEngine;

namespace AlexzanderCowell
{
    public class CharacterMovementScript : MonoBehaviour
    {
        [Header("Character Movement")] private float _mouseXposition,
            mouseSensitivityY,
            mouseSensitivityX,
            _moveHorizontal,
            _moveVertical,
            _mouseYposition;

        [SerializeField] private float runSpeed;
        private CharacterController controller;
        private Vector3 _moveDirection;
        private bool _playerIsJumping;
        private float jumpHeight, characterGravity;
        [SerializeField] private float downValue, upValue;
        private Transform _cameraTransform;
        [SerializeField] private GameObject shoveGameObject;
        [SerializeField] private Transform moveItHere;
        public static bool holdingShovel;
        private int shovelState;

        private void Start()
        {
            _cameraTransform = Camera.main.transform;
            controller = GetComponent<CharacterController>();
            jumpHeight = 2;
            mouseSensitivityY = 0.7f;
            mouseSensitivityX = 1;
            runSpeed = 6;
            _playerIsJumping = false;
            characterGravity = 25;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(holdingShovel);
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

            if (EquipmentScript.canPickUp)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    holdingShovel = true;
                    shovelState += 1;

                    if (shovelState > 1)
                    {
                        shovelState = 0;
                        holdingShovel = false;
                    }
                }
            }

            if (shovelState == 1)
            {
                shoveGameObject.transform.position = moveItHere.position;
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
    }
}
