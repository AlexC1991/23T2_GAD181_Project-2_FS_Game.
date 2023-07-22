using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace AlexzanderCowell
{
    public class NetScript : MonoBehaviour
    {
        private bool _didCatchFireFly;
        private bool _netHitFireFly;
        private bool _fireFlyResult;
        private int _maybeCatchFireFly;
        private float _randomValue;
        private int _outputInt;
        private float _randomIntOutput;
        private float _torchChargeTotal;
        [SerializeField] private int luckyFireFlyCatch = 1;
        [SerializeField] private GameObject nett;
        [SerializeField] private float animationTimer = 0.7f;
        private float _originalAnimationTimer;
        private bool _clickedMouse;


        private void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag("Equipment"))
            {
                Debug.Log("Hitting It");
                _netHitFireFly = true;
            }
        }

        private void Start()
        {
            nett.GetComponent<Animator>().enabled = false;
            _originalAnimationTimer = animationTimer;
            _clickedMouse = false;
        }

        private void Update()
        {
            _maybeCatchFireFly = Random.Range(0, 4);
            _randomValue = Random.Range(0, 8);

            if (_netHitFireFly)
            {
                _outputInt = _maybeCatchFireFly;
                _netHitFireFly = false;
            }

            if (_outputInt == luckyFireFlyCatch)
            {
                _didCatchFireFly = true;
                _outputInt = 0;
            }

            if (_didCatchFireFly)
            {
                _randomIntOutput = _randomValue;
                _torchChargeTotal += _randomIntOutput;
                _didCatchFireFly = false;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _clickedMouse = true;
            }

            if (_clickedMouse)
            {
                nett.GetComponent<Animator>().enabled = true; 
                animationTimer -= 0.6f * Time.deltaTime;
            }

            if (animationTimer < 0.2f)
            {
                _clickedMouse = false;
                nett.GetComponent<Animator>().enabled = false;
                animationTimer = _originalAnimationTimer;
            }
            
            // Debug.Log("Fire Fly Hit Total " +_torchChargeTotal);
            // Debug.Log("Lucky Number Draw " +_outputInt);
            // Debug.Log("Adding This Amount " +_randomIntOutput);
            // Debug.Log("Did the Fire Fly Hit? " +_netHitFireFly);
            // Debug.Log("Did We Catch One? " +_didCatchFireFly);
        }
    }
}
