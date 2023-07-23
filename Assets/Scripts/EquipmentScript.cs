using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class EquipmentScript : MonoBehaviour
    {
        // Declaration of the playerCharacter game object
        [SerializeField] private GameObject playerCharacter;
        [SerializeField] private Camera playerCamera;

        // Declaration of the bool to know if the character is holding an item already
        public static string heldEquipmentName;
        private GameObject hitEquipmentObject;
        public LayerMask mask;

        // Declaration of the text that shows on the UI when a tool is held
        [SerializeField] private TMP_Text equipmentInstructionText;

        // Declaration of the InGameTutorial object
        [SerializeField] private GameObject gameTut;

        // Method updates frequently
        private void Update()
        {
            CheckEquipStatus();
        }

        private void CheckEquipStatus()
        {
            // Check to see if the character is already holding something
            if (CharacterMovementScript.holdingEquipment == false)
            {
                // Declaration of objects used in raycasting
                RaycastHit _hit = new RaycastHit();
                if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out _hit, 5f))
                {
                    // Checks if the object hit with the raycast is an equipment item
                    if (_hit.transform.tag == "Equipment")
                    {
                        // Checks for the key press to pick it up
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            heldEquipmentName = _hit.collider.gameObject.name;
                            hitEquipmentObject = _hit.collider.gameObject;

                            // Swaps the holdingequipment bool, and starts the routine to hold it
                            StartCoroutine(HoldEquipment(hitEquipmentObject));
                            CharacterMovementScript.holdingEquipment = true;
                        }
                    }
                }
            }
        }

        // Routine that moves the equipment with the player character
        private IEnumerator HoldEquipment(GameObject equipment)
        {
            while (true)
            {
                
                heldEquipmentName = equipment.transform.name;
                equipment.transform.position = playerCharacter.GetComponent<CharacterMovementScript>().eqiupmentHoldPosition.transform.position;

                if (heldEquipmentName == "Shovel")
                {
                    playerCharacter.GetComponent<CharacterMovementScript>().equipmentShovel.SetActive(true);
                    equipmentInstructionText.text = "";
                    equipmentInstructionText.gameObject.SetActive(true);

                    // Checks if first time then if it is progresses tutorial stage
                    if (InGameTutorial.firstShovel == true)
                    {
                        InGameTutorial.tutorialAudioSource.Stop();
                        MainMenu.tutorialStage++;
                        InGameTutorial.firstShovel = false;

                        gameTut.GetComponent<InGameTutorial>().RunTutorial();
                    }
                }

                if (heldEquipmentName == "Hammer")
                {
                    playerCharacter.GetComponent<CharacterMovementScript>().equipmentHammer.SetActive(true);
                    equipmentInstructionText.text = "";
                    equipmentInstructionText.gameObject.SetActive(true);

                    if (InGameTutorial.firstShovel == true)
                    {
                        gameTut.GetComponent<InGameTutorial>().NotTheShovel();
                    }
                    else if (InGameTutorial.firstSpade == true)
                    {
                        gameTut.GetComponent<InGameTutorial>().NotTheSpade();
                    }

                    if (MainMenu.tutorialStage >= 7)
                    {
                        if (InGameTutorial.firstHammer == true)
                        {
                            gameTut.GetComponent<InGameTutorial>().FirstHammerPickup();
                            InGameTutorial.firstHammer = false;
                        }
                    }
                }

                if (heldEquipmentName == "Axe")
                {
                    playerCharacter.GetComponent<CharacterMovementScript>().equipmentAxe.SetActive(true);
                    equipmentInstructionText.text = "";
                    equipmentInstructionText.gameObject.SetActive(true);

                    if (InGameTutorial.firstShovel == true)
                    {
                        gameTut.GetComponent<InGameTutorial>().NotTheShovel();
                    }
                    else if (InGameTutorial.firstSpade == true)
                    {
                        gameTut.GetComponent<InGameTutorial>().NotTheSpade();
                    }

                    if (MainMenu.tutorialStage >= 7)
                    {
                        if (InGameTutorial.firstAxe == true) 
                        {
                            gameTut.GetComponent<InGameTutorial>().FirstAxePickup();
                            InGameTutorial.firstAxe = false;
                        }
                    }
                }

                if (heldEquipmentName == "GardenSpade")
                {
                    playerCharacter.GetComponent<CharacterMovementScript>().equipmentSpade.SetActive(true);
                    equipmentInstructionText.text = "";
                    equipmentInstructionText.gameObject.SetActive(true);

                    if (InGameTutorial.firstSpade == true && InGameTutorial.firstShovel == false)
                    {
                        InGameTutorial.tutorialAudioSource.Stop();
                        MainMenu.tutorialStage++;
                        InGameTutorial.firstSpade = false;

                        gameTut.GetComponent<InGameTutorial>().RunTutorial();
                    }
                    if (InGameTutorial.firstShovel == true)
                    {
                        gameTut.GetComponent<InGameTutorial>().NotTheShovel();
                    }
                }

                if (heldEquipmentName == "Net")
                {
                    playerCharacter.GetComponent<CharacterMovementScript>().equipmentNet.SetActive(true);
                    equipmentInstructionText.text = "";
                    equipmentInstructionText.gameObject.SetActive(true);

                    if (InGameTutorial.firstShovel == true)
                    {
                        gameTut.GetComponent<InGameTutorial>().NotTheShovel();
                    }
                    else if (InGameTutorial.firstSpade == true)
                    {
                        gameTut.GetComponent<InGameTutorial>().NotTheSpade();
                    }

                    if (MainMenu.tutorialStage >= 7)
                    {
                        if (InGameTutorial.firstNet == true)
                        {
                            gameTut.GetComponent<InGameTutorial>().FirstNetPickup();
                            InGameTutorial.firstNet = false;
                        }
                    }
                }
                CheckEquipmentDrop();

                yield return null;
            }
        }

        private void CheckEquipmentDrop()
        {
            // Checks to make sure the player isnt holding equipment
            if (CharacterMovementScript.holdingEquipment == true)
            {
                // Checks to see if the player presses the e key
                if (Input.GetKeyDown(KeyCode.E))
                {

                    // The following if statements check what equipment was just dropped and turns the character holding it off
                    if (heldEquipmentName == "Shovel")
                    {
                        playerCharacter.GetComponent<CharacterMovementScript>().equipmentShovel.SetActive(false);
                    }

                    if (heldEquipmentName == "Hammer")
                    {
                        playerCharacter.GetComponent<CharacterMovementScript>().equipmentHammer.SetActive(false);
                    }

                    if (heldEquipmentName == "Axe")
                    {
                        playerCharacter.GetComponent<CharacterMovementScript>().equipmentAxe.SetActive(false);
                    }

                    if (heldEquipmentName == "GardenSpade")
                    {
                        playerCharacter.GetComponent<CharacterMovementScript>().equipmentSpade.SetActive(false);
                    }

                    if (heldEquipmentName == "Net")
                    {
                        playerCharacter.GetComponent<CharacterMovementScript>().equipmentNet.SetActive(false);
                    }
                    
                    // Resets all of the variables for the equipment checking
                    heldEquipmentName = null;
                    StopAllCoroutines();
                    equipmentInstructionText.text = "";
                    equipmentInstructionText.gameObject.SetActive(false);
                    CharacterMovementScript.holdingEquipment = false;

                    InGameTutorial.audioSpacerInt = 0;
                }
            }
        }
    }
}

