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
        [SerializeField] private GameObject tutorialOnIndicator;
        [SerializeField] private GameObject tutorialOffIndicator;
        [SerializeField] private GameObject tutorial;
        private bool FirststartUI;

        private void Start()
        {
            _InGameMenuUI.SetActive(false); // In Game UI Starts False.
            controlsScreen.SetActive(true);
            LowMouseSensitivity(); // Low Mouse Sensitivity Open Selected At Start.
            TutorialOn();
            FirststartUI = false;
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

            if (FirststartUI)
            {
                _InGameMenuUI.SetActive(true);
                FirststartUI = false;
            }
            if (escState == 1 && !_InGameMenuUI.activeInHierarchy && !controlsScreen.activeInHierarchy)// If ESC Int Equals to 1 Then It will Produce the In Game Menu.
            {
                FirststartUI = true;
                Time.timeScale = 0; // Sets Game Speed To Null.
                Cursor.lockState = CursorLockMode.Confined; // Confines the Cursor on the Screen.
                Cursor.visible = true; // Allows the Cursor to be displayed in game.
            }
            else if (escState == 0)
            {
                _InGameMenuUI.SetActive(false); // If ESC Int Isn't Equal to 1 Then It will Close The In Game Menu.
                controlsScreen.SetActive(false);
                Time.timeScale = 1; // Sets Game Speed To Normal Speed.
                Cursor.lockState = CursorLockMode.Locked; // Confines the Cursor on the Screen and locks it in place.
                Cursor.visible = false; // Stops the Cursor being displayed in game.
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
            _InGameMenuUI.SetActive(false);
            controlsScreen.SetActive(true);
        }

        public void BackToInGameMenu()
        {
            _InGameMenuUI.SetActive(true);
            controlsScreen.SetActive(false);
        }

        public void TutorialOn()
        {
            tutorial.SetActive(true);
            tutorialOnIndicator.SetActive(true);
            tutorialOffIndicator.SetActive(false);
        }
        
        public void TutorialOff()
        {
            tutorial.SetActive(false);
            tutorialOnIndicator.SetActive(false);
            tutorialOffIndicator.SetActive(true);
        }
        
    }
}