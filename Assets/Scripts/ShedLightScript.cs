using AlexzanderCowell;
using UnityEngine;

public class ShedLightScript : MonoBehaviour
{
    [SerializeField] private GameObject shedLight;
    private float _currentTime;

    private void Update()
    {
        _currentTime = WorldClock.hourTime;

        if (_currentTime > 16 || _currentTime < 5)
        {
            shedLight.GetComponent<Light>().intensity = 20f;
        }

        if (_currentTime > 5 && _currentTime < 16)
        {
            shedLight.GetComponent<Light>().intensity = 1f;
        }
    }
}
