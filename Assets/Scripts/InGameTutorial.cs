using AlexzanderCowell;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameTutorial : MonoBehaviour
{
    // Declaration of all the audio clips for the tutorial
    [Header("Audio Voice Lines")]
    [SerializeField] public static AudioSource tutorialAudioSource;
    [SerializeField] private AudioClip tutorialVoiceLine1;
    [SerializeField] private AudioClip tutorialVoiceLine2;
    [SerializeField] private AudioClip notTheShovel;
    [SerializeField] private AudioClip tutorialVoiceLine3;
    [SerializeField] private AudioClip notTheSpade;
    [SerializeField] private AudioClip tutorialVoiceLine4;
    [SerializeField] private AudioClip tutorialVoiceLine5;
    [SerializeField] private AudioClip tutorialVoiceLine6;
    [SerializeField] private AudioClip tutorialVoiceLine7;
    [SerializeField] private AudioClip tutorialVoiceLine8;
    [SerializeField] private AudioClip tutorialVoiceLine9;
    [SerializeField] private AudioClip firstAxeVoiceLine;
    [SerializeField] private AudioClip firstHammerVoiceLine;

    public static AudioClip publicNotTheShovel;
    public static AudioClip publicNotTheSpade;
    public static AudioClip publicFirstAxe;
    public static AudioClip publicFirstHammer;

    // Declaration of the images of the farmer
    [Header("Farmer Image Settings")]
    [SerializeField] private GameObject farmerImageObject;
    public AnimationCurve myCurve;

    // Declaration of booleans used to track if its the first time someone has used a tool
    public static bool firstMove;
    public static bool firstShovel;
    public static bool lastShovel;
    public static bool firstSpade;
    public static bool lastSpade;
    public static bool firstAxe;
    public static bool firstHammer;

    // Plays at the start
    private void Start()
    {
        tutorialAudioSource = GetComponentInChildren<AudioSource>();

        RunTutorial();

        firstMove = true;
        firstShovel = true;
        lastShovel = true;
        firstSpade = true;
        lastSpade = true;
        firstAxe = true;
        firstHammer = true;

        publicNotTheShovel = notTheShovel;
        publicNotTheSpade = notTheSpade;
        publicFirstAxe = firstAxeVoiceLine;
        publicFirstHammer = firstHammerVoiceLine;
    }

    // updates every frame
    private void Update()
    {
        // Shakes the farmer image when speaking
        if (tutorialAudioSource.isPlaying)
        {
            farmerImageObject.transform.position = new Vector3(transform.position.x, myCurve.Evaluate((Time.time % myCurve.length)) + 150f, transform.position.z);
        }

        //Debug.Log(MainMenu.tutorialStage);
    }

    // Handles all of the turotial audio clips playing
    public void RunTutorial()
    {
        Debug.Log("running tutorial method");
        if (MainMenu.tutorialStage == 0)
        {
            tutorialAudioSource.PlayOneShot(tutorialVoiceLine1);
        }

        if (MainMenu.tutorialStage == 1)
        {
            tutorialAudioSource.PlayOneShot(tutorialVoiceLine2);
        }

        if (MainMenu.tutorialStage == 2)
        {
            tutorialAudioSource.PlayOneShot(tutorialVoiceLine3);
        }

        if (MainMenu.tutorialStage == 3)
        {
            tutorialAudioSource.PlayOneShot(tutorialVoiceLine4);
            MainMenu.tutorialStage++;
        }

        if (MainMenu.tutorialStage == 4 && tutorialAudioSource.isPlaying == false)
        {
            tutorialAudioSource.PlayOneShot(tutorialVoiceLine5);
        }

        if (MainMenu.tutorialStage == 5)
        {
            tutorialAudioSource.PlayOneShot(tutorialVoiceLine6);
        }

        if (MainMenu.tutorialStage == 6)
        {
            tutorialAudioSource.PlayOneShot(tutorialVoiceLine7);
        }

        if (MainMenu.tutorialStage == 7)
        {
            tutorialAudioSource.PlayOneShot(tutorialVoiceLine8);
        }

        if (MainMenu.tutorialStage == 8)
        {
            tutorialAudioSource.PlayOneShot(tutorialVoiceLine9);
        }
    }

    public void NotTheShovel()
    {
        tutorialAudioSource.PlayOneShot(notTheShovel);
    }   

    public void NotTheSpade()
    {
        tutorialAudioSource.PlayOneShot(notTheSpade);
    }

    public void EndTutorial()
    {
        Destroy(this);
    }
}
