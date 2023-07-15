using UnityEngine;

namespace AlexzanderCowell
{
    public class S3Potatos : MonoBehaviour
    {
        private bool _canCollect; // If Player Can collect or not.
        [SerializeField] private GameObject dirtPatch; // Dirt Prefab
        [SerializeField] private GameObject witheredPatch; // Withered Prefab
        private float currentTimeOfPlanting; // Tracks time of what the world time is currently at.
        private float startPlantTime; // Sets time of which the instance is initialized.
        [SerializeField] private GameObject displayText; // Displays Message to Harvest On Prefab.
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) // If Player is close to collider it will allow set the collect bool to yes & Display the text To Harvest.
            {
                _canCollect = true;
                displayText.SetActive(true);
            }
        }
        private void Start()
        {
            startPlantTime = WorldClock.hourTime; // Sets float to whatever the current hour time is set at during the start phase of this prefab is spawned.
            displayText.SetActive(false); // Does not Start the Text Straight Away.
            gameObject.SetActive(true);
        }
        private void Update()
        {
            currentTimeOfPlanting = WorldClock.hourTime; // Tracks the world time hour as a variable with in the script.
            
            if (_canCollect && Input.GetKeyDown(KeyCode.C)) // If you can collect is true & you press C.
            {
                Vector3 xyz = new Vector3(-90, 0, 0); // Makes a xyz Vector 3 variable to use below.
                Quaternion newRotation = Quaternion.Euler(xyz); // using the xyz it is used in a Quaternion variable called newRotation.
                
                SeedStorage.potatos += 2; // When collecting it will give you 2 potato's for your total potato storage.
                SeedStorage.potatoSeedOutPut += 0.4f; // When collecting you will gain 0.4f per potato you collect to go towards a potato seed pool on the seed storage script.
                Instantiate(dirtPatch, transform.position, newRotation); // Spawns in dirt prefab with the current transform.position with a rotation of the newRotation variable from above.
                gameObject.SetActive(false); // Destroys S3 Potato Stage Prefab in scene.
            }

            if (currentTimeOfPlanting > startPlantTime + 3) // If the current time is more then + 3 in hours of when this was first spawn in then it will change to being withered.
            {
                Vector3 xyz = new Vector3(-90, 0, 0);
                Quaternion newRotation = Quaternion.Euler(xyz);
                Instantiate(witheredPatch, transform.position, newRotation);
                gameObject.SetActive(false);
            }
        }
    }
}
