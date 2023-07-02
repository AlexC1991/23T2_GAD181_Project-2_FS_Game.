using UnityEngine;

namespace AlexzanderCowell
{
    public class EquipmentScript : MonoBehaviour
    {
        public static bool canPickUp;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                canPickUp = true;
            }
        }
    }
}
