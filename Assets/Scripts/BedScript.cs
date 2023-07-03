using System;
using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{


    public class BedScript : MonoBehaviour
    {
        [SerializeField] private GameObject blackSleepScreen;
        private float timerToRestAgain;
        private bool ableToSleep;
        [HideInInspector] public bool addMoreTime;
        private bool startSleeping;
        private bool resetTimer;
        private bool buttonPressedE;

        [Header("Bed Ready Or Not Text")] 
        [SerializeField] private GameObject readyOrNot;
        [SerializeField] private Text readyOrNotText;

        private void Start()
        {
            readyOrNotText.text = ("Not Ready");               
            readyOrNot.GetComponent<Text>().color = Color.red; 
            timerToRestAgain = 15;
            addMoreTime = false;
            startSleeping = false;
            blackSleepScreen.GetComponent<CanvasGroup>().alpha = 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ableToSleep = true;
            }
        }

        private void Update()
        {
            timerToRestAgain -= 1f * Time.deltaTime;
            
            if (ableToSleep && Input.GetKeyDown(KeyCode.T) && timerToRestAgain < 0.2f && buttonPressedE)
            {
                startSleeping = true;
                blackSleepScreen.GetComponent<CanvasGroup>().alpha = 1;
                addMoreTime = true;
                Time.timeScale = 0;
                buttonPressedE = false;
                ableToSleep = false;
            }
            
            if (resetTimer)
            {
                timerToRestAgain = 15;
                readyOrNotText.text = ("Not Ready");                     
                readyOrNot.GetComponent<Text>().color = Color.red; 
                resetTimer = false;
            }

            if (timerToRestAgain < 0.2f)
            {
                timerToRestAgain = 0;
                readyOrNotText.text = ("Ready");
                readyOrNot.GetComponent<Text>().color = Color.blue;
                buttonPressedE = true;
            }
            if (startSleeping)
            {
                resetTimer = true;
                blackSleepScreen.GetComponent<CanvasGroup>().alpha -= 0.3f * Time.unscaledDeltaTime;
            } 
            
            if (blackSleepScreen.GetComponent<CanvasGroup>().alpha < 0.2f && startSleeping)
            {
                Time.timeScale = 1;
                startSleeping = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ableToSleep = false;
            }
        }

    }
}
