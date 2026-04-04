using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_menu : MonoBehaviour
{
    public void play()
    {
        SceneManager.LoadScene(1); //game scene
    }

    public void quit() //quits the game when player clicks on quit button in menu
    {
        Application.Quit();
    }
}
