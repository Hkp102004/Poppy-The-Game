using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float waitTime;
    [SerializeField] private Text score;
    [SerializeField] private Sprite[] lives_images; //array of lives display
    [SerializeField] private Image lives_displayer;
    [SerializeField] private GameObject gameover_Screen;
    [SerializeField] private AudioSource CollectionSound;
    [SerializeField] private AudioSource GameOverSound;
    [SerializeField] private AudioSource DamageSound;
    [SerializeField] private GameObject gamewinScreen;
    [SerializeField] private AudioSource gamewinSound;
    [SerializeField] private GameObject pausemenu;
    [SerializeField] private GameObject shieldIcon;
    [SerializeField] private AudioSource bgmusic;
    spawner spawn;
    playerBehaviour player;
    private int scorevar=0;
    private Coroutine blinkCoroutine;
    void Start()
    {
        spawn = GameObject.FindGameObjectWithTag("Spawner").GetComponent<spawner>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerBehaviour>();

        gameover_Screen.gameObject.SetActive(false);
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
        if(CollectionSound == null)
        {
            Debug.LogError("Collection sound is missing from UIManager script");
            return;
        }
        if(GameOverSound == null)
        {
            Debug.LogError("The GameoverSound is missing in UIManager script");
            return;
        }
        if(DamageSound == null)
        {
            Debug.LogError("The damage sound is missing in UIManager script");
            return;
        }
        if(gamewinScreen == null)
        {
            Debug.LogError("Game win screen is missing in UIManager script");
            return;
        }
        if(gamewinSound == null)
        {
            Debug.LogError("Game win sound is not in uimanager script");
            return;
        }
        if(pausemenu == null)
        {
            Debug.LogError("Pause menu is missing in UIManager script");
            return;
        }
        if(shieldIcon == null)
        {
            Debug.LogError("Shield icon is missing in UIManager script");
            return;
        }
        if(bgmusic == null)
        {
            Debug.LogError("Background music is missing in UIManager script");
            return;
        }
        shieldIcon.gameObject.SetActive(true);
        gamewinScreen.gameObject.SetActive(false);
        lives_displayer.sprite = lives_images[3];
        pausemenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        score.text = scorevar.ToString();

    }

    public void collection()
    {
        CollectionSound.Play();;
        scorevar+=1;
    }

    public void UpdateLive(int lives)
    {
        lives_displayer.sprite = lives_images[lives];
        DamageSound.Play();
    }

    public void DeadScreen()
    {
        gameover_Screen.gameObject.SetActive(true);
        GameOverSound.Play();
    }

    public void WinScreen()
    {
        bgmusic.Stop(); //this will stop the background music when game ends
        gamewinSound.Play();
        gamewinScreen.gameObject.SetActive(true);
    }

    public void pause()
    {
        Time.timeScale = 0f;
        pausemenu.gameObject.SetActive(true);
        player.deactive();
        // spawn.StopSpawning();
    }
    
    public void resume()
    {
        Time.timeScale = 1f;
        pausemenu.gameObject.SetActive(false);
        player.active();
    }

    public void showShieldIcon() //function to make the shield icon visible
    {
        shieldIcon.gameObject.SetActive(true);
    }

    public void hideShieldIcon() //function to make the shield icon invisible
    {
        shieldIcon.gameObject.SetActive(false);
    }

    public void BlinkShield()
    {
        blinkCoroutine = StartCoroutine(BlinkShieldIcon());
    }

    public void StopBlinkingShield()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }
        shieldIcon.gameObject.SetActive(true);
    }

    IEnumerator BlinkShieldIcon()
    {
        while(true)
        {
            shieldIcon.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            shieldIcon.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
