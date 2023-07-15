using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class S1CarrotScript : MonoBehaviour
    {
        [SerializeField] private GameObject s2CarrotStage; // Prefab for S2 Carrot Stage.
        private float currentTimeOfPlanting; // Tracks time of what the world time is currently at.
        private float startPlantTime; // Sets time of which the instance is initialized.
        private float nextPlantStageTime;

        [Header("Progress Bar Settings")]
        [SerializeField] private Slider progressBar;
        private float barValue;

        private void Start()
        {
            startPlantTime = WorldClock.hourTime; // Sets float to whatever the current hour time is set at during the start phase of this prefab is spawned.
            nextPlantStageTime = startPlantTime + 2;
            gameObject.SetActive(true);

            progressBar.maxValue = nextPlantStageTime - startPlantTime;
            StartCoroutine("ProgressBar");
        }
        private void Update()
        {
            currentTimeOfPlanting = WorldClock.hourTime; // Tracks the world time hour as a variable with in the script.

            if (nextPlantStageTime > 24)
            {
                nextPlantStageTime -= 24;
            }

            if (currentTimeOfPlanting > nextPlantStageTime) // If the current time is more then + 2 in hours of when this was first spawn in then it will change to S2 Carrot Stage.
            {
                Instantiate(s2CarrotStage, transform.position, transform.rotation);
                //gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }

        private IEnumerator ProgressBar()
        {
            while (true)
            {
                while (barValue <= progressBar.maxValue)
                {
                    barValue = nextPlantStageTime - currentTimeOfPlanting * 0.01f;
                    UpdateProgressBar();

                    yield return null;
                }
                
                yield return null;
            }
        }

        private void UpdateProgressBar()
        {
            progressBar.value = barValue;
        }
    }
}
