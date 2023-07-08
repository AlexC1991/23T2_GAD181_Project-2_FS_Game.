using UnityEngine;

namespace AlexzanderCowell
{
    public class EquipmentScript : MonoBehaviour
    {
        public static bool canPickUp;

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                canPickUp = true;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                canPickUp = false;
            }
        }
    }
}
