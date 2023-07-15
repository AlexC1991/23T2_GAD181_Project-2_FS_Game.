using System;
using UnityEngine;

namespace AlexzanderCowell
{
    public class CarScript : MonoBehaviour
    {
        private float destroyTimer = 3;

        private void Start()
        {
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (CarSpawner.finishedWithCar)
            {
                gameObject.SetActive(false);
            }

            if (!gameObject.activeInHierarchy)
            {
                CarSpawner.finishedWithCar = false;
                Destroy(gameObject, destroyTimer);
            }
        }
    }
}
