using UnityEngine;
using UnityEngine.SceneManagement;

public class BeforeGameScreen : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("FarmingSite1");
    }
}
