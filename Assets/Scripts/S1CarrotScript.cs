using UnityEngine;

namespace AlexzanderCowell
{
    public class S1CarrotScript : MonoBehaviour
    {
        [SerializeField] private GameObject s2CarrotStage;
        private float currentTimeOfPlanting;
        private float startPlantTime;

        private void Start()
        {
            startPlantTime = WorldClock.hourTime;
        }
        private void Update()
        {
            currentTimeOfPlanting = WorldClock.hourTime;

            if (currentTimeOfPlanting > startPlantTime + 2)
            {
                Instantiate(s2CarrotStage, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
