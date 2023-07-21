using AlexzanderCowell;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeforeGameScreen : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip tutorialVoiceLine1;
    [SerializeField] private AudioClip tutorialVoiceLine2;
    [SerializeField] private AudioClip tutorialVoiceLine3;
    [SerializeField] private AudioClip tutorialVoiceLine4;

    private void Start()
    {
        audioSource.PlayOneShot(tutorialVoiceLine1);

        MainMenu.tutorialStage += 1;

        StartCoroutine("Tutorial");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Debug.Log("tutorial stage is = " + MainMenu.tutorialStage);
            Debug.Log("Mosue is clicked");
            audioSource.Stop();

            if (MainMenu.tutorialStage == 4)
            {
                StartGame();
            }
        }
    }

    private IEnumerator Tutorial()
    {
        while (true)
        {
            Debug.Log("Tutorial is running from top");
            if (MainMenu.tutorialStage == 1 && audioSource.isPlaying == false)
            {
                audioSource.PlayOneShot(tutorialVoiceLine2);
                MainMenu.tutorialStage += 1;
                Debug.Log("tutorial stage is = " + MainMenu.tutorialStage);
            }

            if (MainMenu.tutorialStage == 2 && audioSource.isPlaying == false)
            {
                audioSource.PlayOneShot(tutorialVoiceLine3);
                MainMenu.tutorialStage += 1;
                Debug.Log("tutorial stage is = " + MainMenu.tutorialStage);
            }

            if (MainMenu.tutorialStage == 3 && audioSource.isPlaying == false)
            {
                audioSource.PlayOneShot(tutorialVoiceLine4);
                MainMenu.tutorialStage += 1;
                Debug.Log("tutorial stage is = " + MainMenu.tutorialStage);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("FarmingSite1");
    }
}
