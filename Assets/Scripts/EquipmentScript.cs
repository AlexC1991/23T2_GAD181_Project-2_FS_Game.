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

        public LayerMask mask;

        // Method updates frequently
        private void Update()
        {
            Debug.Log("holdingequipment = " + holdingEquipment);
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
                        Debug.Log("the player is looking");
                        // Checks for the key press to pick it up
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            // Swapps the holdingequipment bool, and starts the routine to hold it
                            holdingEquipment = true;
                            StartCoroutine(HoldEquipment(this.gameObject));
                        }
                    }
                } 
            }
            else if (holdingEquipment == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StopCoroutine(HoldEquipment(this.gameObject));
                    holdingEquipment = false;
                }
            }
        }

        // Routine that moves the equipmentr with the palyer character
        private IEnumerator HoldEquipment(GameObject equipment)
        {
            while (true)
            {                
                if (holdingEquipment == true)
                {
                    equipment.transform.LookAt(playerCharacter.GetComponent<CharacterMovementScript>().equipmentHoldDirection.transform);
                    equipment.transform.position = playerCharacter.GetComponent<CharacterMovementScript>().eqiupmentHoldPosition.transform.position;
                }
                if (holdingEquipment == false)
                {
                    break;
                }
                yield return null;
            }
        }
    }
}

