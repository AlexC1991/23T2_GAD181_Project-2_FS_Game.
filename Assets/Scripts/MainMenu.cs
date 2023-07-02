using UnityEngine;
using UnityEngine.SceneManagement;

namespace AlexzanderCowell
{
    public class MainMenu : MonoBehaviour
    {
        public void StartBeforeGameScene()
        {
            SceneManager.LoadScene("BeforeLoadingIn");
        }
    }
}
