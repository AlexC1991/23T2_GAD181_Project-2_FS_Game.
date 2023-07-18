using System;
using UnityEngine;

namespace AlexzanderCowell
{
    public class CarScript : MonoBehaviour
    {
        private float destroyTimer = 3;

        //Declarartion of the AudioSource for SFX
        [SerializeField] private AudioSource carSFX;

        private void Start()
        {
            gameObject.SetActive(true);

            // Sets the settings for each audio source attached to the car and plays the audio
            carSFX.volume = 0.15f;
            carSFX.loop = true;
            carSFX.minDistance = 2f;
            carSFX.maxDistance = 9f;
            carSFX.rolloffMode = AudioRolloffMode.Logarithmic;
            carSFX.spatialBlend = 1f;
            carSFX.Play();
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
                
                // Stops the audio once a car is about to be destroyed
                carSFX.Stop();


                Destroy(gameObject, destroyTimer);
            }
        }
    }
}
