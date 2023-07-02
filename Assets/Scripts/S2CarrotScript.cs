using UnityEngine;

namespace AlexzanderCowell
{
    public class S2CarrotScript : MonoBehaviour
    {
        [SerializeField] private GameObject s3CarrotStage;
        private float currentTimeOfPlanting;
        private float startPlantTime;

        private void Start()
        {
            startPlantTime = WorldClock.hourTime;
        }
        private void Update()
        {
            currentTimeOfPlanting = WorldClock.hourTime;

            if (currentTimeOfPlanting > startPlantTime + 5)
            {
                Instantiate(s3CarrotStage, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
