using UnityEngine;

namespace AlexzanderCowell
{
    public class InGameMenu : MonoBehaviour
    {

        [SerializeField] private GameObject _InGameMenuUI; // In Game Menu UI Screen Image.
        [SerializeField] private GameObject _lowMouseSensitiveIndicator; // In Game Menu UI Low Mouse Sensitivity Button Indicator.
        [SerializeField] private GameObject _medMouseSensitiveIndicator; // In Game Menu UI Medium Mouse Sensitivity Button Indicator.
        [SerializeField] private GameObject _highMouseSensitiveIndicator; // In Game Menu UI High Mouse Sensitivity Button Indicator.
        [SerializeField] private CharacterMovementScript cMoveScript; // Character Movement Script.
        private int escState; // Int Counter For ESC Button.

        // Declarartion of the variables needed for the in game controls screen
        [SerializeField] private GameObject controlsScreen;
        [SerializeField] private GameObject uiScreenMaster;
        private bool viewingControlScreen;

        private void Start()
        {
            _InGameMenuUI.SetActive(false); // In Game UI Starts False.
            LowMouseSensitivity(); // Low Mouse Sensitivity Open Selected At Start.

            viewingControlScreen = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) // Pressing ESC Button.
            {
                escState += 1; // Adds + 1 every ESC Press.

                if (escState > 1) // If ESC Int Is More Than 1 It Reverts to 0.
                {
                    escState = 0;
                }
            }

            if (escState == 1) // If ESC Int Equals to 1 Then It will Produce the In Game Menu.
            {
                _InGameMenuUI.SetActive(true); // In Game Menu UI Show.
                Time.timeScale = 0; // Sets Game Speed To Null.
                Cursor.lockState = CursorLockMode.Confined; // Confines the Curser on the Screen.
                Cursor.visible = true; // Allows the Cursor to be displayed in game.
            }
            else
            {
                _InGameMenuUI.SetActive(false); // If ESC Int Isn't Equal to 1 Then It will Close The In Game Menu.
                Time.timeScale = 1; // Sets Game Speed To Normal Speed.
                Cursor.lockState = CursorLockMode.Locked; // Confines the Curser on the Screen and locks it in place.
                Cursor.visible = false; // Stops the Cursor being displayed in game.

                controlsScreen.SetActive(false);
                uiScreenMaster.SetActive(true);
                _InGameMenuUI.SetActive(false);
            }
        }

        public void HighMouseSensitivity() // High Mouse Sensitivity Option For Button.
        {
            cMoveScript.mouseSensitivityY = 3f; // Sets the Movement Script Mouse SensitivityY to 3.
            cMoveScript.mouseSensitivityX = 4; // Sets the Movement Script Mouse SensitivityX to 4.
            _lowMouseSensitiveIndicator.SetActive(false); // Turns off the Low Mouse Sensitivity Indicator.
            _medMouseSensitiveIndicator.SetActive(false); // Turns off the Medium Mouse Sensitivity Indicator.
            _highMouseSensitiveIndicator.SetActive(true); // Turns on the High Mouse Sensitivity Indicator.
        }
        
        public void MedMouseSensitivity() // Medium Mouse Sensitivity Option For Button.
        {
            cMoveScript.mouseSensitivityY = 1.5f; // Sets the Movement Script Mouse SensitivityY to 1.5.
            cMoveScript.mouseSensitivityX = 2.5f; // Sets the Movement Script Mouse SensitivityX to 2.5.
            _lowMouseSensitiveIndicator.SetActive(false); // Turns off the Low Mouse Sensitivity Indicator.
            _medMouseSensitiveIndicator.SetActive(true); // Turns on the Medium Mouse Sensitivity Indicator.
            _highMouseSensitiveIndicator.SetActive(false); // Turns off the High Mouse Sensitivity Indicator.
        }
        
        public void LowMouseSensitivity() // Low Mouse Sensitivity Option For Button.
        {
            cMoveScript.mouseSensitivityY = 0.8f; // Sets the Movement Script Mouse SensitivityY to 0.8.
            cMoveScript.mouseSensitivityX = 1; // Sets the Movement Script Mouse SensitivityX to 1.
            _lowMouseSensitiveIndicator.SetActive(true); // Turns on the Low Mouse Sensitivity Indicator.
            _medMouseSensitiveIndicator.SetActive(false); // Turns off the Medium Mouse Sensitivity Indicator.
            _highMouseSensitiveIndicator.SetActive(false); // Turns off the High Mouse Sensitivity Indicator.
        }

        public void ToggleControls()
        {
            if (viewingControlScreen == true)
            {
                controlsScreen.SetActive(false);
                uiScreenMaster.SetActive(true);
                _InGameMenuUI.SetActive(false);

                viewingControlScreen = false;
            }
            else if (viewingControlScreen == false)
            {
                controlsScreen.SetActive(true);
                uiScreenMaster.SetActive(false);
                _InGameMenuUI.SetActive(false);

                viewingControlScreen = true;
            }
        }
    }
}