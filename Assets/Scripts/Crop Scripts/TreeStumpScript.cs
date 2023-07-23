using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class TreeStumpScript : MonoBehaviour
    {
        [SerializeField] private GameObject treeStage2; // Prefab for S3 Carrot Stage.
        private float currentTimeOfPlanting; // Tracks time of what the world time is currently at.
        [SerializeField] private float startPlantTime; // Sets time of which the instance is initialized.
        private float nextPlantStageTime;

        /*[Header("Progress Bar Settings")]*/
        /*[SerializeField] private Slider progressBar;*/
        /*private float barValue;*/

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

            if (currentTimeOfPlanting > nextPlantStageTime) // If the current time is more then + 5 in hours of when this was first spawn in then it will change to S3 Carrot Stage.
            {
                Vector3 xyz = new Vector3(-90, 0, 0); // Makes a xyz Vector 3 variable to use below.
                Quaternion newRotation = Quaternion.Euler(xyz); // using the xyz it is used in a Quaternion variable called newRotation.
                SeedStorage.wood += 1; // When collecting it will give you 1 carrot for your total carrot storage.
                Instantiate(treeStage2, transform.position, newRotation); // Spawns in dirt prefab with the current transform.position with a rotation of the newRotation variable from above.
                Destroy(gameObject); //Destroys Tree Prefab in scene.
            }

            /*UpdateProgressBar();*/
        }

        /*private void UpdateProgressBar()
        {
            barValue += progressBar.minValue + (Time.deltaTime * WorldClock.timeMultiplier * 0.00027f);
            progressBar.value = barValue;
        }*/
    }
}
