using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class WorldClock : MonoBehaviour
    {
        public static float hourTime;
        private float minuteTime;
        public static float secondsTime;
        public static float timeMultiplier;
        

        [Header("Scripts")] 
        [SerializeField] private BedScript bScript;
        
        [Header("World Time Clock")]
        [SerializeField] private Text hourTimeDisplayed;
        [SerializeField] private Text minTimeDisplayed;
        [SerializeField] private Text secTimeDisplayed;

        [Header("World Clock Speed Indicators")] 
        [SerializeField] private GameObject pauseG;
        [SerializeField] private GameObject playG;
        [SerializeField] private GameObject fast2G;
        [SerializeField] private GameObject fast3G;
        [SerializeField] private GameObject pauseText;

        [Header("World Day Cycle Indicator")] 
        [SerializeField] private GameObject sunEmote;
        [SerializeField] private GameObject moonEmote;

        [Header(("World Light Control"))] 
        [SerializeField] private Light topLight;
        [SerializeField] private Light light1;
        [SerializeField] private Light light2;
        [SerializeField] private Light light3;
        [SerializeField] private Light light4;
        private void Start()
        {
            timeMultiplier = 0.8f;
            hourTime = 8;
            minuteTime = 0;
            secondsTime = 0;
            pauseG.SetActive(false);
            playG.SetActive(true);
            fast2G.SetActive(false);
            fast3G.SetActive(false);
            Time.timeScale = 1;
            pauseText.SetActive(false);
        }
        private void Update()
        {
            hourTimeDisplayed.text = (hourTime).ToString("00");
            minTimeDisplayed.text = (minuteTime).ToString("00");
            secTimeDisplayed.text = (secondsTime).ToString("00");
            
            secondsTime += 1 * timeMultiplier * Time.deltaTime;

            if (secondsTime > 59.8f)
            {
                minuteTime += 1;
                secondsTime = 0;
            }

            if (minuteTime > 59.8f)
            {
                hourTime += 1;
                minuteTime = 0;
            }

            if (hourTime > 23.8f)
            {
                hourTime = 0;
            }

            if (hourTime > 6 && hourTime < 18)
            {
                sunEmote.SetActive(true);
                moonEmote.SetActive(false);
            }
            else
            {
                sunEmote.SetActive(false);
                moonEmote.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                pauseG.SetActive(false);
                playG.SetActive(true);
                fast2G.SetActive(false);
                fast3G.SetActive(false);
                timeMultiplier = 1f;
                Time.timeScale = 1;
                pauseText.SetActive(false);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                pauseG.SetActive(false);
                playG.SetActive(false);
                fast2G.SetActive(true);
                fast3G.SetActive(false);
                timeMultiplier = 15f;
                Time.timeScale = 1;
                pauseText.SetActive(false);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                pauseG.SetActive(false);
                playG.SetActive(false);
                fast2G.SetActive(false);
                fast3G.SetActive(true);
                timeMultiplier = 45f;
                Time.timeScale = 1;
                pauseText.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                pauseG.SetActive(true);
                playG.SetActive(false);
                fast2G.SetActive(false);
                fast3G.SetActive(false);
                timeMultiplier = 0.8f;
                Time.timeScale = 0;
                pauseText.SetActive(true);
            }

            if (hourTime > 7 && hourTime < 9)
            {
                LightSettingEight();
            }
            if (hourTime > 9 && hourTime < 11)
            {
                LightSettingSeven();
            } 
            if (hourTime > 11 && hourTime < 13)
            {
                LightSettingSix();
            }
            if (hourTime > 13 && hourTime < 15)
            {
                LightSettingFive();
            }
            if (hourTime > 15 && hourTime < 17)
            {
                LightSettingFour();
            }
            if (hourTime > 17 && hourTime < 19)
            {
                LightSettingThree();
            }
            if (hourTime > 19 && hourTime < 21)
            {
                LightSettingTwo();
            }
            if (hourTime > 21 && hourTime < 23)
            {
                LightSettingOne();
            }
            if (hourTime > 1 && hourTime < 3)
            {
                LightSettingThree();
            }
            if (hourTime > 3 && hourTime < 5)
            {
                LightSettingFive();
            }
            if (hourTime > 5 && hourTime < 7)
            {
                LightSettingSeven();
            }

            if (bScript.addMoreTime)
            {
                hourTime += 4;
                bScript.addMoreTime = false;
            }
        }

        private void LightSettingOne()
        {   topLight.intensity = 0.1f; 
            light1.intensity = 0.1f;
            light2.intensity = 0.1f;
            light3.intensity = 0.1f;
            light4.intensity = 0.1f;
        }

        private void LightSettingTwo()
        {
            topLight.intensity = 0.2f; 
            light1.intensity = 0.2f;
            light2.intensity = 0.2f;
            light3.intensity = 0.2f;
            light4.intensity = 0.2f;
        }
        private void LightSettingThree()
        {
            topLight.intensity = 0.3f; 
            light1.intensity = 0.3f;
            light2.intensity = 0.3f;
            light3.intensity = 0.3f;
            light4.intensity = 0.3f;
        }
        
        private void LightSettingFour()
        {
            topLight.intensity = 0.4f; 
            light1.intensity = 0.4f;
            light2.intensity = 0.4f;
            light3.intensity = 0.4f;
            light4.intensity = 0.4f;
        }
        private void LightSettingFive()
        {
            topLight.intensity = 0.5f; 
            light1.intensity = 0.5f;
            light2.intensity = 0.5f;
            light3.intensity = 0.5f;
            light4.intensity = 0.5f;
        }
        private void LightSettingSix()
        {
            topLight.intensity = 0.6f; 
            light1.intensity = 0.6f;
            light2.intensity = 0.6f;
            light3.intensity = 0.6f;
            light4.intensity = 0.6f;
        }
        private void LightSettingSeven()
        {
            topLight.intensity = 0.7f; 
            light1.intensity = 0.7f;
            light2.intensity = 0.7f;
            light3.intensity = 0.7f;
            light4.intensity = 0.7f;
        }
        private void LightSettingEight()
        {
            topLight.intensity = 0.8f; 
            light1.intensity = 0.8f;
            light2.intensity = 0.8f;
            light3.intensity = 0.8f;
            light4.intensity = 0.8f;
        }
    }
}
