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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(paused) resume();
            else Pause();
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(1); // 1 is for game scene
    }
    public void Main_menu()
    {
        SceneManager.LoadScene(0); //0 is for main menu scene
        resume();
    }
    void Pause() //to check and pause the game
    {
        paused = true;
        ui.pause();
    }

    public void resume()
    {
        paused = false;
        ui.resume();
    }
}
