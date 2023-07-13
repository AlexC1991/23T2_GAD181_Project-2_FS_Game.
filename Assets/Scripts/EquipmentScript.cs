using System.Collections;
using UnityEngine;

namespace AlexzanderCowell
{
    public class EquipmentScript : MonoBehaviour
    {
        [SerializeField] private GameObject playerCharacter;

        public static bool holdingEquipment = false;

        private void Update()
        {
            if (holdingEquipment)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    holdingEquipment = false;
                    StopCoroutine(HoldEquipment(this.gameObject));
                }
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject == playerCharacter)
            {
                Debug.Log("Collision with palyer");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!holdingEquipment)
                    {
                        holdingEquipment = true;
                        this.gameObject.GetComponent<BoxCollider>().enabled = false;
                        StartCoroutine(HoldEquipment(this.gameObject));
                    }
                    else
                    {
                        holdingEquipment = false;
                        this.gameObject.GetComponent<BoxCollider>().enabled = true;
                        StopCoroutine(HoldEquipment(this.gameObject));
                    }
                }
            }
        }

        private IEnumerator HoldEquipment(GameObject equipment)
        {
            while (true)
            {
                equipment.transform.rotation = Quaternion.Euler(playerCharacter.transform.rotation.x, playerCharacter.transform.rotation.y, playerCharacter.transform.rotation.z);
                equipment.transform.position = playerCharacter.GetComponent<CharacterMovementScript>().eqiupmentHoldPosition.transform.position;
                yield return null;
            }
        }
    }
}
