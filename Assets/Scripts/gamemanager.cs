using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(1); // 1 is for game scene
    }
    public void Main_menu()
    {
        SceneManager.LoadScene(0); //0 is for main menu scene
    }
}
