using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Text messageText;
    [SerializeField] private float waitTime;
    [SerializeField] private Text score;
    [SerializeField] private Sprite[] lives_images; //array of lives display
    [SerializeField] private Image lives_displayer;
    [SerializeField] private GameObject gameover_Screen;
    spawner spawn;
    playerBehaviour player;
    private int scorevar=0;
    void Start()
    {
        spawn = GameObject.FindGameObjectWithTag("Spawner").GetComponent<spawner>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerBehaviour>();

        messageText.gameObject.SetActive(false);
        gameover_Screen.gameObject.SetActive(false);
        if(messageText==null)
        {
            Debug.LogError("message text is missing from uimanager script");
            return;
        }
        if(score==null)
        {
            Debug.LogError("The score text or memory crystal text is missing in uiscript");
            return;
        }
        if(spawn==null)
        {
            Debug.LogError("Spawner script is missing in UIManager script");
            return;
        }
        if(player==null)
        {
            Debug.LogError("playerBehaviour script is missing in uimanager script");
            return;
        }
        if(lives_displayer==null)
        {
            Debug.LogError("Lives displayer is missing in uimanager");
            return;
        }
        lives_displayer.sprite = lives_images[3];
    }

    // Update is called once per frame
    void Update()
    {
        score.text = scorevar.ToString();

    }

    public void ShowMessage(string msg)
    {
        messageText.text = msg;
        messageText.gameObject.SetActive(true);
        scorevar+=1;
        spawn.StopSpawning();
        StartCoroutine(MessageCooldown());
    }

    IEnumerator MessageCooldown()
    {
        yield return new WaitForSeconds(waitTime);
        messageText.gameObject.SetActive(false);
        spawn.RestartSpawn(); //this will start spawning again
    }

    public void UpdateLive(int lives)
    {
        lives_displayer.sprite = lives_images[lives];
    }

    public void DeadScreen()
    {
        gameover_Screen.gameObject.SetActive(true);
    }
}
