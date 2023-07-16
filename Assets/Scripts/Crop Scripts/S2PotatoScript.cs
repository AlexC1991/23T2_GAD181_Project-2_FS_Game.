using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class S2PotatoScript : MonoBehaviour
    {
        [SerializeField] private GameObject s3PotatoStage; // Prefab for S3 Potato Stage.
        private float currentTimeOfPlanting; // Tracks time of what the world time is currently at.
        [SerializeField] private float startPlantTime; // Sets time of which the instance is initialized.
        private float nextPlantStageTime;

        [Header("Progress Bar Settings")]
        [SerializeField] private Slider progressBar;
        private float barValue;

        private void Start()
        {
            startPlantTime = WorldClock.hourTime; // Sets float to whatever the current hour time is set at during the start phase of this prefab is spawned.
            nextPlantStageTime = startPlantTime + 3;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            currentTimeOfPlanting = WorldClock.hourTime; // Tracks the world time hour as a variable with in the script.

            if (nextPlantStageTime >= 24)
            {
                nextPlantStageTime -= 24;
            }

            if (currentTimeOfPlanting > nextPlantStageTime) // If the current time is more then + 3 in hours of when this was first spawn in then it will change to S3 Potato Stage.
            {
                Instantiate(s3PotatoStage, transform.position, transform.rotation);
                gameObject.SetActive(false);
            }

            UpdateProgressBar();
        }

        private void UpdateProgressBar()
        {
            barValue += progressBar.minValue + (Time.deltaTime * WorldClock.timeMultiplier * 0.00027f);
            progressBar.value = barValue;
        }
    }
}
