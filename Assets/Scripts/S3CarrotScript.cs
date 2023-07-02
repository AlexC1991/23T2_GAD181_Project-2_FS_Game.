using UnityEngine;

namespace AlexzanderCowell
{
    public class S3CarrotScript : MonoBehaviour
    {
        private bool _canCollect;
        [SerializeField] private GameObject dirtPatch;
        [SerializeField] private GameObject witheredPatch;
        private float currentTimeOfPlanting;
        private float startPlantTime;
        [SerializeField] private GameObject displayText;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _canCollect = true;
                displayText.SetActive(true);
            }
        }
        private void Start()
        {
            startPlantTime = WorldClock.hourTime;
            displayText.SetActive(false);
        }
        private void Update()
        {
            currentTimeOfPlanting = WorldClock.hourTime;
            
            if (_canCollect && Input.GetKeyDown(KeyCode.C))
            {
                Vector3 xyz = new Vector3(-90, 0, 0);
                Quaternion newRotation = Quaternion.Euler(xyz);
                SeedStorage.carrots += 1;
                SeedStorage.carrotSeedOutput += 0.8f;
                Instantiate(dirtPatch, transform.position, newRotation);
                Destroy(gameObject);
            }
 
            if (currentTimeOfPlanting > startPlantTime + 3)
            {
                Vector3 xyz = new Vector3(-90, 0, 0);
                Quaternion newRotation = Quaternion.Euler(xyz);
                Instantiate(witheredPatch, transform.position, newRotation);
                Destroy(gameObject);
            }
        }
    }
}
