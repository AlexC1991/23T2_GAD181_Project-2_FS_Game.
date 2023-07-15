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
        public static bool holdingEquipment = false;
        public static string heldEquipmentName;
        private GameObject hitEquipmentObject;

        public LayerMask mask;

        // Method updates frequently
        private void Update()
        {
            // Check to see if the character is already holding something
            if (holdingEquipment == false)
            {
                // Declaration of objects used in raycasting
                RaycastHit _hit = new RaycastHit();
                if (Physics.Raycast(playerCharacter.transform.position, playerCharacter.transform.forward, out _hit, 5f))
                {
                    // Checks if the object hit with the raycast is an equipment item
                    if (_hit.transform.tag == "Equipment")
                    {
                        //Debug.Log("the player is looking");
                        // Checks for the key press to pick it up
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            heldEquipmentName = _hit.transform.name;
                            hitEquipmentObject = _hit.transform.gameObject;

                            // Swaps the holdingequipment bool, and starts the routine to hold it
                            StartCoroutine(HoldEquipment(this.gameObject));
                            holdingEquipment = true;
                        }
                    }
                } 
            }
            // Checks to make sure the player isnt holding equipment
            else if (holdingEquipment == true)
            {
               // Checks to see if the player presses the e key
               if (Input.GetKeyDown(KeyCode.E))
               {
                    // Stops the routine that holds the equipment
                    StopAllCoroutines();
                    StopCoroutine(HoldEquipment(this.gameObject));
                    holdingEquipment = false;

                    // Checks if the item was a shovel and if it was turn off the shovel
                    if (heldEquipmentName == "Shovel")
                    {
                        playerCharacter.GetComponent<CharacterMovementScript>().equipmentShovel.SetActive(false);
                        heldEquipmentName = null;
                    }

                    if (heldEquipmentName == "Hammer")
                    {
                        playerCharacter.GetComponent<CharacterMovementScript>().equipmentHammer.SetActive(false);
                        heldEquipmentName = null;
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

                yield return null;
            }
        }
    }
}

