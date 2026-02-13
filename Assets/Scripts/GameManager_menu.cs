using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_menu : MonoBehaviour
{
    public void play()
    {
        SceneManager.LoadScene(1); //game scene
    }
}
