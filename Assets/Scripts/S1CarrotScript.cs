using UnityEngine;

namespace AlexzanderCowell
{
    public class S1CarrotScript : MonoBehaviour
    {
        [SerializeField] private GameObject s2CarrotStage; // Prefab for S2 Carrot Stage.
        private float currentTimeOfPlanting; // Tracks time of what the world time is currently at.
        private float startPlantTime; // Sets time of which the instance is initialized. 

        private void Start()
        {
            startPlantTime = WorldClock.hourTime; // Sets float to whatever the current hour time is set at during the start phase of this prefab is spawned.
            gameObject.SetActive(true);
        }
        private void Update()
        {
            currentTimeOfPlanting = WorldClock.hourTime; // Tracks the world time hour as a variable with in the script.

            if (currentTimeOfPlanting > startPlantTime + 2) // If the current time is more then + 2 in hours of when this was first spawn in then it will change to S2 Carrot Stage.
            {
                Instantiate(s2CarrotStage, transform.position, transform.rotation);
                gameObject.SetActive(false);
            }
        }
    }
}
