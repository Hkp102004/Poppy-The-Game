using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(1); // 1 is for game scene
    }
    public void Main_menu()
    {
        SceneManager.LoadScene(0); //0 is for main menu scene
    }
}
