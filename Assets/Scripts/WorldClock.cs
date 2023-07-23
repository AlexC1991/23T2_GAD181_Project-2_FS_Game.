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
        [SerializeField] private GameObject rainEmote;

        [Header(("World Light Control"))] 
        [SerializeField] private Light topLight;
        [SerializeField] private Light light1;
        [SerializeField] private Light light2;
        [SerializeField] private Light light3;
        [SerializeField] private Light light4;
        [SerializeField] private GameObject fireBugs;
        [SerializeField] private Material daySkybox;
        [SerializeField] private Material nightSkybox;
        [SerializeField] private Material stormyWeatherSkyBox;

        // Declaration of variable needed for the ambience
        [Header("Ambiance Controls")]
        [SerializeField] private AudioSource ambienceSFXSource;
        [SerializeField] private AudioClip daytimeAmbience;
        [SerializeField] private AudioClip nightAmbience;
        [SerializeField] private AudioClip stormyAmbience;

        private void Start()
        {
            RenderSettings.skybox = daySkybox;
            fireBugs.SetActive(false);
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
                fireBugs.SetActive(false);
            }
            if (hourTime > 9 && hourTime < 11)
            {
                LightSettingSeven();
                fireBugs.SetActive(false);
            } 
            if (hourTime > 11 && hourTime < 13)
            {
                LightSettingSix();
                fireBugs.SetActive(false);
            }
            if (hourTime > 13 && hourTime < 15)
            {
                LightSettingFive();
                fireBugs.SetActive(false);
            }
            if (hourTime > 15 && hourTime < 17)
            {
                LightSettingFour();
                fireBugs.SetActive(false);
            }
            if (hourTime > 17 && hourTime < 19)
            {
                LightSettingThree();
                fireBugs.SetActive(false);
            }
            if (hourTime > 19 && hourTime < 21)
            {
                LightSettingTwo();
                fireBugs.SetActive(true);
            }
            if (hourTime > 21 && hourTime < 23)
            {
                LightSettingOne();
                fireBugs.SetActive(false);
            }
            if (hourTime > 1 && hourTime < 3)
            {
                LightSettingThree();
                fireBugs.SetActive(true);
            }
            if (hourTime > 3 && hourTime < 5)
            {
                LightSettingFour();
                fireBugs.SetActive(false);
            }
            if (hourTime > 5 && hourTime < 7)
            {
                LightSettingSix();
                fireBugs.SetActive(false);
            }

            if (bScript.addMoreTime)
            {
                hourTime += 3;
                bScript.addMoreTime = false;
            }

            if (hourTime > 5 && hourTime < 16 && !WeatherScript.isRainingNow)
            {
                RenderSettings.skybox = daySkybox;
               
                if (ambienceSFXSource.isPlaying == false || ambienceSFXSource.clip != daytimeAmbience)
                {
                    sunEmote.SetActive(true);
                    moonEmote.SetActive(false);
                    rainEmote.SetActive(false);
                    ambienceSFXSource.clip = daytimeAmbience;
                    ambienceSFXSource.Play();
                }
            }
            
            if (hourTime > 16 || hourTime < 5 && !WeatherScript.isRainingNow)
            {
                RenderSettings.skybox = nightSkybox;
                
                if (ambienceSFXSource.isPlaying == false || ambienceSFXSource.clip != nightAmbience)
                {
                    sunEmote.SetActive(false);
                    moonEmote.SetActive(true);
                    rainEmote.SetActive(false);
                    ambienceSFXSource.clip = nightAmbience;
                    ambienceSFXSource.Play();
                }
            }

            if (WeatherScript.isRainingNow)
            {
                RenderSettings.skybox = stormyWeatherSkyBox;
                
                if (ambienceSFXSource.isPlaying == false || ambienceSFXSource.clip != stormyAmbience)
                {
                    sunEmote.SetActive(false);
                    moonEmote.SetActive(false);
                    rainEmote.SetActive(true);
                    ambienceSFXSource.clip = stormyAmbience;
                    ambienceSFXSource.Play();
                }
            }
        }

        private void LightSettingOne()
        {   topLight.intensity = 0.05f; 
            light1.intensity = 0.05f;
            light2.intensity = 0.05f;
            light3.intensity = 0.05f;
            light4.intensity = 0.05f;
        }

        private void LightSettingTwo()
        {
            topLight.intensity = 0.1f; 
            light1.intensity = 0.1f;
            light2.intensity = 0.1f;
            light3.intensity = 0.1f;
            light4.intensity = 0.1f;
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
