using UnityEngine;

namespace AlexzanderCowell
{
    public class S2CarrotScript : MonoBehaviour
    {
        [SerializeField] private GameObject s3CarrotStage; // Prefab for S3 Carrot Stage.
        private float currentTimeOfPlanting; // Tracks time of what the world time is currently at.
        private float startPlantTime; // Sets time of which the instance is initialized.

        private void Start()
        {
            startPlantTime = WorldClock.hourTime; // Sets float to whatever the current hour time is set at during the start phase of this prefab is spawned.
        }
        private void Update()
        {
            currentTimeOfPlanting = WorldClock.hourTime; // Tracks the world time hour as a variable with in the script.

            if (currentTimeOfPlanting > startPlantTime + 5) // If the current time is more then + 5 in hours of when this was first spawn in then it will change to S3 Carrot Stage.
            {
                Instantiate(s3CarrotStage, transform.position, Quaternion.identity); 
                Destroy(gameObject);
            }
        }
    }
}
