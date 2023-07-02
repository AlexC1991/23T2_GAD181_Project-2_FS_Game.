using UnityEngine;

namespace AlexzanderCowell
{
    public class S1PotatoScript : MonoBehaviour
    {
        [SerializeField] private GameObject s2PotatoStage;
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
                Instantiate(s2PotatoStage, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
