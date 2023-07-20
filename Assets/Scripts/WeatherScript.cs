using UnityEngine;

namespace AlexzanderCowell
{


    public class WeatherScript : MonoBehaviour
    {
        [SerializeField] private GameObject rainEnabled;
        private int _randomWeather;
        private int _currentWeatherState;
        private float _currentHourTime;
        private float _previousHourTime;
        public static bool isRainingNow;

        private void Start()
        {
            isRainingNow = false;
            rainEnabled.SetActive(false);
            _previousHourTime = WorldClock.hourTime + 2;
            _currentWeatherState = _randomWeather;
        }

        private void Update()
        {
            Debug.Log("Weather State: " + _currentWeatherState);
            Debug.Log("Weather Previous Time: " + _previousHourTime);
            Debug.Log("Weather Current Time: " + _currentHourTime);
            _currentHourTime = WorldClock.hourTime;
            _randomWeather = Random.Range(0,5);
            
             if (_previousHourTime >= 24)
             {
                 _previousHourTime -= 24;
             }
            
            if (_currentHourTime > _previousHourTime)
            {
                _currentWeatherState = _randomWeather;
                _previousHourTime = _currentHourTime + 3;
            }

            if (_currentWeatherState == 1)
            {
                StartRain();
            }
            else
            {
                SunnyWeather();
            }

            if (rainEnabled.activeInHierarchy)
            {
                isRainingNow = true;
            }
            else
            {
                isRainingNow = false;
            }
        }

        private void StartRain()
        {
            rainEnabled.SetActive(true);
        }

        private void SunnyWeather()
        {
            isRainingNow = false;
            rainEnabled.SetActive(false);
        }
    }
}
