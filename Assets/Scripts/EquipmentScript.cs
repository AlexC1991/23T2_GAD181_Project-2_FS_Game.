using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace AlexzanderCowell
{
    public class EquipmentScript : MonoBehaviour
    {
        // Declaration of the playerCharacter game object
        [SerializeField] private GameObject playerCharacter;

        // Declaration of the bool to know if the character is holding an item already
        public static string heldEquipmentName;
        private GameObject hitEquipmentObject;

        public LayerMask mask;

        // Method updates frequently
        private void Update()
        {
            CheckEquipStatus();

            if (Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log("holding equpment = " + CharacterMovementScript.holdingEquipment);
            }
        }

        private void CheckEquipStatus()
        {
            // Check to see if the character is already holding something
            if (CharacterMovementScript.holdingEquipment == false)
            {
                // Declaration of objects used in raycasting
                RaycastHit _hit = new RaycastHit();
                if (Physics.Raycast(playerCharacter.transform.position, playerCharacter.transform.forward, out _hit, 5f, mask))
                {
                    // Checks if the object hit with the raycast is an equipment item
                    if (_hit.transform.tag == "Equipment")
                    {
                        //Debug.Log("the player is looking");
                        // Checks for the key press to pick it up
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            heldEquipmentName = _hit.collider.gameObject.name;
                            hitEquipmentObject = _hit.collider.gameObject;

                            Debug.Log("heldEquipmentName = " + heldEquipmentName);
                            Debug.Log("hitEquipmentObject = " + hitEquipmentObject);

                            // Swaps the holdingequipment bool, and starts the routine to hold it
                            Debug.Log("starting routine " + hitEquipmentObject);
                            StartCoroutine(HoldEquipment(hitEquipmentObject));
                            CharacterMovementScript.holdingEquipment = true;
                            Debug.Log("holding equipment = " + CharacterMovementScript.holdingEquipment);
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
                }

                if (heldEquipmentName == "Hammer")
                {
                    playerCharacter.GetComponent<CharacterMovementScript>().equipmentHammer.SetActive(true);
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
                    // Stops the routine that holds the equipment
                    Debug.Log("stopping routine " + hitEquipmentObject + ".");
                    Debug.Log("holding equipment = " + CharacterMovementScript.holdingEquipment);

                    // Checks if the item was a shovel and if it was turn off the shovel
                    if (heldEquipmentName == "Shovel")
                    {
                        playerCharacter.GetComponent<CharacterMovementScript>().equipmentShovel.SetActive(false);
                        heldEquipmentName = null;
                        StopCoroutine(HoldEquipment(hitEquipmentObject));
                        CharacterMovementScript.holdingEquipment = false;
                    }

                    if (heldEquipmentName == "Hammer")
                    {
                        playerCharacter.GetComponent<CharacterMovementScript>().equipmentHammer.SetActive(false);
                        heldEquipmentName = null;
                        StopCoroutine(HoldEquipment(hitEquipmentObject));
                        CharacterMovementScript.holdingEquipment = false;
                    }
                    
                    
                }
            }
        }
    }
}

