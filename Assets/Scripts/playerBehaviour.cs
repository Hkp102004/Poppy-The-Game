using System.Collections;
using UnityEngine;


public class playerBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float height = 400f;
    [SerializeField] private GameObject bulletPrefab; //prefaab of the bullet that will be instantiated
    [SerializeField] private float firerate = 0.5f;
    private int jumpcount = 0;
    private int maxjump = 2;
    [SerializeField] private int lives = 3; //this is for lives of player
    [SerializeField] private Animator animator; //this is for the animation
    [SerializeField] private float shootdelay = 0.3f;
    [SerializeField] private bool shieldactive = false;
    [SerializeField] private GameObject shield;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource shieldRecharge;
    [SerializeField] private AudioSource ShootingSound;
    [SerializeField] private AudioSource shieldSound;
    private bool alive;
    private bool invincible;
    UIManager ui;
    spawner spawnerScript;

    void Start()
    {
       animator = GetComponent<Animator>();
       ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
       spawnerScript = GameObject.FindGameObjectWithTag("Spawner").GetComponent<spawner>();
       shield.gameObject.SetActive(false);
       shieldactive = true;
       jumpSound = GetComponent<AudioSource>(); //this will get the audio source
       if(bulletPrefab==null)
        {
            Debug.LogError("Bullet prefab is missing in playerBehaviour script");
            return;
        } 
        if(body == null)
        {
            Debug.LogError("Rigidbody2D is missing in playerBehvaiour scipt");
            return;
        }
        if(animator == null)
        {
            Debug.LogError("Animator is missing in playerBehaviour script");
            return;
        }
        if(ui == null)
        {
            Debug.LogError("UIManager script is missing from playerBehaviour script");
            return;
        }
        if(spawnerScript == null)
        {
            Debug.LogError("Spawner script is missing in player script");
            return;
        }
        if(shield == null)
        {
            Debug.LogError("Shield is missing in playerBehaviour script");
            return;
        }
        if(shieldRecharge == null)
        {
            Debug.LogError("Shield recharge audio source is missing in playerBehaviour script");
            return;
        }
        if(ShootingSound == null)
        {
            Debug.LogError("Shooting sould or audio source is missing in playerBehavior script");
            return;
        }
        if(shieldSound == null)
        {
            Debug.LogError("Shield sound is missing in playerNehaviour script");
            return;
        }
        alive = true;
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
        Shield();
        WinCheck();
    }

    public void Movement()
    {
        float horiInput = Input.GetAxis("Horizontal"); //key maps for fonrizontal inputs

        Vector3 direction = new Vector3(horiInput,0,0);
        if(alive)
        {
            transform.Translate(direction * speed * Time.deltaTime); //the movement  
        }

        if(transform.position.x <= -3.5f)
        {
            transform.position = new Vector3(-3.5f, transform.position.y, transform.position.z);
        }

        if(Input.GetKeyDown(KeyCode.Space) && jumpcount<maxjump && alive)
        {
            body.linearVelocity = new Vector3(body.linearVelocityX,0,0);
            jumpSound.Play();
            body.AddForce(Vector3.up * height, ForceMode2D.Impulse);
            jumpcount++;
        }

        if(horiInput > 0.1f && alive) //this is the animation for movement 
        {
            animator.ResetTrigger("reset");
            animator.ResetTrigger("left");
            animator.SetTrigger("right");
        }
        else if(horiInput < -0.1f  && alive)
        {
            animator.ResetTrigger("reset");
            animator.ResetTrigger("right");
            animator.SetTrigger("left");
        }
        else
        {
            animator.ResetTrigger("right");
            animator.ResetTrigger("left");
            animator.SetTrigger("reset");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Ground")
        {
            jumpcount=0;
        }
    }

    public void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.E) && firerate <= 0 &&alive)
        {
            animator.SetTrigger("shoot");  //triggering the shooting animation
            StartCoroutine(ShootingDelay(shootdelay));  
            firerate = 0.5f;
        }
        else
        {
            firerate -= Time.deltaTime;
        }
    }

    public void Damage()
    {
        if(lives>0 && !invincible && alive) //bug should be fixed here
        {
            lives--;
            ui.UpdateLive(lives);
        }
        if(lives==0)
        {
            // Destroy(gameObject);
            alive = false;
            ui.DeadScreen();
            spawnerScript.StopSpawning();
        }
    }

    public void Shield()  
    {
        if(Input.GetKeyDown(KeyCode.Q) && shieldactive && alive)
        {
            shield.gameObject.SetActive(true);
            shieldSound.Play();
            invincible = true;
            ui.BlinkShield();
            StartCoroutine(ShieldOverload());
            StartCoroutine(ShieldCooldown());
        }
    }

    public void WinCheck() //checks if player won the game  
    {
       if(transform.position.x >= 258.3f)
        {
            alive = false;
            ui.WinScreen();
        }
    }

    public void active() //to make players movement active 
    {
        alive = true;
    }

    public void deactive() //to make players movement deactive
    {
        alive = false;
    }

    IEnumerator ShootingDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShootingSound.Play();
        Instantiate(bulletPrefab, transform.position + new Vector3(0.9f,0.35f,0), Quaternion.Euler(0,0,90));
    }

    IEnumerator ShieldOverload()  
    {
        yield return new WaitForSeconds(4f);
        shield.gameObject.SetActive(false);
        ui.StopBlinkingShield();
        ui.hideShieldIcon();
        shieldactive = false;
        invincible = false;
    }
    
    IEnumerator ShieldCooldown()
    {
        yield return new WaitForSeconds(10f);
        shieldactive = true;
        ui.showShieldIcon();
        shieldRecharge.Play();
    }

    public void powerup()
    {
        if(lives==3)
        {
            height += 100;
        }
        else
        {
            lives++;
            ui.UpdateLive(lives);
        }
    }
}
