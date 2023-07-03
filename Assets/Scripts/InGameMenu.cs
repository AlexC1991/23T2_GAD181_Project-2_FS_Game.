using UnityEngine;

namespace AlexzanderCowell
{
    public class InGameMenu : MonoBehaviour
    {

        [SerializeField] private GameObject _InGameMenuUI;
        [SerializeField] private GameObject _lowMouseSensitiveIndicator;
        [SerializeField] private GameObject _medMouseSensitiveIndicator;
        [SerializeField] private GameObject _highMouseSensitiveIndicator;
        [SerializeField] private CharacterMovementScript cMoveScript;
        private int escState;


        private void Start()
        {
            _InGameMenuUI.SetActive(false);
            LowMouseSensitivity();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                escState += 1;

                if (escState > 1)
                {
                    escState = 0;
                }
            }

            if (escState == 1)
            {
                _InGameMenuUI.SetActive(true);
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else
            {
                _InGameMenuUI.SetActive(false);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        public void HighMouseSensitivity()
        {
            cMoveScript.mouseSensitivityY = 3f;
            cMoveScript.mouseSensitivityX = 4;
            _lowMouseSensitiveIndicator.SetActive(false);
            _medMouseSensitiveIndicator.SetActive(false);
            _highMouseSensitiveIndicator.SetActive(true);
        }
        
        public void MedMouseSensitivity()
        {
            cMoveScript.mouseSensitivityY = 1.5f;
            cMoveScript.mouseSensitivityX = 2.5f;
            _lowMouseSensitiveIndicator.SetActive(false);
            _medMouseSensitiveIndicator.SetActive(true);
            _highMouseSensitiveIndicator.SetActive(false);
        }
        
        public void LowMouseSensitivity()
        {
            cMoveScript.mouseSensitivityY = 0.8f;
            cMoveScript.mouseSensitivityX = 1;
            _lowMouseSensitiveIndicator.SetActive(true);
            _medMouseSensitiveIndicator.SetActive(false);
            _highMouseSensitiveIndicator.SetActive(false);
        }
    }
}