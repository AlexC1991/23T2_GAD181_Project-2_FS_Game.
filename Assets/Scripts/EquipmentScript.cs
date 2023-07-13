using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace AlexzanderCowell
{
    public class EquipmentScript : MonoBehaviour
    {
        [SerializeField] private GameObject playerCharacter;

        public static bool holdingEquipment = false;

        public LayerMask mask;

        private void Update()
        {
            Debug.Log("holdingequipment = " + holdingEquipment);
            if (holdingEquipment == false)
            {
                RaycastHit _hit = new RaycastHit();
                if (Physics.Raycast(playerCharacter.transform.position, playerCharacter.transform.forward, out _hit, 10f))
                {
                    if (_hit.transform.tag == "Equipment")
                    {
                        Debug.Log("the player is looking");
                        if (Input.GetKeyDown(KeyCode.E))
                        {
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

        private IEnumerator HoldEquipment(GameObject equipment)
        {
            while (true)
            {                
                if (holdingEquipment == true)
                {
                    equipment.transform.rotation = Quaternion.Euler(playerCharacter.transform.rotation.x, playerCharacter.transform.rotation.y, playerCharacter.transform.rotation.z);
                    equipment.transform.position = playerCharacter.GetComponent<CharacterMovementScript>().eqiupmentHoldPosition.transform.position;
                }
                yield return null;
            }
        }
    }
}
