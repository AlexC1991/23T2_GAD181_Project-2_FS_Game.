using UnityEngine;
using UnityEngine.Serialization;

namespace AlexzanderCowell
{


    public class S2PotatoScript : MonoBehaviour
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

            if (currentTimeOfPlanting > startPlantTime + 3)
            {
                Instantiate(s3CarrotStage, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
