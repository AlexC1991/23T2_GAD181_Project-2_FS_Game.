using System;
using UnityEngine;

namespace AlexzanderCowell
{
    
    public class WaterScript : MonoBehaviour
    {
        [HideInInspector] public static bool allowedToFish;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                allowedToFish = true;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                allowedToFish = false;
            }
        }
    }
}
