using UnityEngine;

namespace AlexzanderCowell
{
    public class S1PotatoScript : MonoBehaviour
    {
        [SerializeField] private GameObject s2PotatoStage; // Prefab for S2 Potato Stage.
        private float currentTimeOfPlanting; // Tracks time of what the world time is currently at.
        [SerializeField] private float startPlantTime; // Sets time of which the instance is initialized. 
        private float nextPlantStageTime;

        private void Start()
        {
            startPlantTime = WorldClock.hourTime; // Sets float to whatever the current hour time is set at during the start phase of this prefab is spawned.
            nextPlantStageTime = startPlantTime + 2;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            currentTimeOfPlanting = WorldClock.hourTime; // Tracks the world time hour as a variable with in the script.


            if (nextPlantStageTime >= 24)
            {
                nextPlantStageTime -= 24;
            }

            if (currentTimeOfPlanting > nextPlantStageTime) // If the current time is more then + 2 in hours of when this was first spawn in then it will change to S2 Potato Stage.
            {
                Instantiate(s2PotatoStage, transform.position, transform.rotation);
                gameObject.SetActive(false);
            }
        }
    }
}
