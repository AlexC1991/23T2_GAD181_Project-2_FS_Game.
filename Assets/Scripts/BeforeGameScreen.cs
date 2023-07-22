using AlexzanderCowell;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeforeGameScreen : MonoBehaviour
{
    // Declaration of all the audio clips for the tutorial
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip tutorialVoiceLine1;
    [SerializeField] private AudioClip tutorialVoiceLine2;
    [SerializeField] private AudioClip tutorialVoiceLine3;
    [SerializeField] private AudioClip tutorialVoiceLine4;

    // Declaration of the images of the farmer
    [SerializeField] private GameObject farmerImageObject;
    public AnimationCurve myCurve;

    // Plays at the start
    private void Start()
    {
        RunTutorial();
    }

    // Plays once a frame
    private void Update()
    {   
        // Checks to see if the mouse is clicked
        if (Input.GetMouseButtonDown(0)) 
        {
            // Stops the audio clip and runs the tutorial method
            audioSource.Stop();
            RunTutorial();

            // If the tutorial stage is 4 then it starts the game on click
            if (MainMenu.tutorialStage == 4 && audioSource.isPlaying == false)
            {
                StartGame();
            }
        }

        // Shakes the farmer image when speaking
        if (audioSource.isPlaying) 
        {
            farmerImageObject.transform.position = new Vector3(transform.position.x, myCurve.Evaluate((Time.time % myCurve.length)) + 530, transform.position.z);
        }
    }

    // Method that runs the tutorial audio
    private void RunTutorial()
    {
        Debug.Log("Tutorial is running from top");
        if (MainMenu.tutorialStage == 0)
        {
            audioSource.PlayOneShot(tutorialVoiceLine1);
            MainMenu.tutorialStage += 1;
        }

        if (MainMenu.tutorialStage == 1 && audioSource.isPlaying == false)
        {
            audioSource.PlayOneShot(tutorialVoiceLine2);
            MainMenu.tutorialStage += 1;
        }

        if (MainMenu.tutorialStage == 2 && audioSource.isPlaying == false)
        {
            audioSource.PlayOneShot(tutorialVoiceLine3);
            MainMenu.tutorialStage += 1;
        }

        if (MainMenu.tutorialStage == 3 && audioSource.isPlaying == false)
        {
            audioSource.PlayOneShot(tutorialVoiceLine4);
            MainMenu.tutorialStage += 1;
        }
    }

    // Method that starts the game
    public void StartGame()
    {
        audioSource.Stop();
        MainMenu.tutorialStage = 0;
        SceneManager.LoadScene("FarmingSite1");
    }
}
