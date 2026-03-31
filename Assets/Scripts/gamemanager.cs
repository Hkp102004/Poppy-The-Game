using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class gamemanager : MonoBehaviour
{
    UIManager ui; //ui manager script reference
    bool paused;

    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        if(ui == null)
        {
            Debug.LogError("the uimanager script is missing from gamemanager script");
            return;
        }
    }

    void Update()
    {
        Pause();
    }
    public void Restart()
    {
        SceneManager.LoadScene(1); // 1 is for game scene
    }
    public void Main_menu()
    {
        SceneManager.LoadScene(0); //0 is for main menu scene
    }
    void Pause() //to check and pause the game
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            paused = true;
            ui.pause();

            if(paused)
            {
                paused = false;
                ui.resume();
            }
        }
    }

    void resume()
    {
        paused = false;
        ui.resume();
    }
}
