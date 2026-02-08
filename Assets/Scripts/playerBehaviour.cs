using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField] public int lives = 3;
    [SerializeField] private Animator animator; //this is for the animation
    [SerializeField] private float shootdelay = 0.3f;

    void Start()
    {
        animator = GetComponent<Animator>();
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
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
    }

    public void Movement()
    {
        float horiInput = Input.GetAxis("Horizontal"); //key maps for fonrizontal inputs

        Vector3 direction = new Vector3(horiInput,0,0);
        transform.Translate(direction * speed * Time.deltaTime);

        if(transform.position.x <= -3.5f)
        {
            transform.position = new Vector3(-3.5f, transform.position.y, transform.position.z);
        }

        if(Input.GetKeyDown(KeyCode.Space) && jumpcount<maxjump)
        {
            body.linearVelocity = new Vector3(body.linearVelocityX,0,0);
            body.AddForce(Vector3.up * height, ForceMode2D.Impulse);
            jumpcount++;
            animator.SetTrigger("jump"); //triggering the jumpting animation
        }

        if(horiInput > 0.1f) //this is the animation for movement 
        {
            animator.ResetTrigger("reset");
            animator.ResetTrigger("left");
            animator.SetTrigger("right");
        }
        else if(horiInput < -0.1f)
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
        if(Input.GetKeyDown(KeyCode.E) && firerate <= 0)
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
        if(lives>0)
        {
            lives--;
        }
        if(lives==0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ShootingDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(bulletPrefab, transform.position + new Vector3(0.9f,0.35f,0), Quaternion.Euler(0,0,90));
    }

}
