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
        public static float _torchChargeTotal;
        [SerializeField] private int luckyFireFlyCatch = 1;
        [SerializeField] private GameObject nett;
        [SerializeField] private float animationTimer = 0.7f;
        private float _originalAnimationTimer;
        private bool _clickedMouse;
        public static bool addPowerToTorch;


        private void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag("Net"))
            {
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
            _torchChargeTotal = Mathf.Clamp(_torchChargeTotal, 0, 90);
            
            if (_torchChargeTotal < 0.2f && TorchScript.batteryBank > 0)
            {
                _torchChargeTotal = 0;
            }
            
            _maybeCatchFireFly = Random.Range(0, 7);
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
                addPowerToTorch = false;
            }

            if (animationTimer < 0.2f)
            {
                addPowerToTorch = true;
                _clickedMouse = false;
                nett.GetComponent<Animator>().enabled = false;
                animationTimer = _originalAnimationTimer;
            }
            
            Debug.Log("Fire Fly Hit Total " +_torchChargeTotal);
            // Debug.Log("Lucky Number Draw " +_outputInt);
            // Debug.Log("Adding This Amount " +_randomIntOutput);
            // Debug.Log("Did the Fire Fly Hit? " +_netHitFireFly);
            // Debug.Log("Did We Catch One? " +_didCatchFireFly);
        }
    }
}
