using UnityEngine;
using UnityEngine.SceneManagement;

namespace AlexzanderCowell
{
    public class MainMenu : MonoBehaviour
    {
        static public int tutorialStage = 0;
        
        public void StartBeforeGameScene()
        {
            SceneManager.LoadScene("BeforeLoadingIn");
        }
    }
}
