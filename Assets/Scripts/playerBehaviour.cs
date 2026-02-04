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
    //[SerializeField] private bool Jumpie = true;
    private int jumpcount = 0;
    private int maxjump = 2;
    void Start()
    {
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
        if(Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0.9f,0,0), Quaternion.Euler(0,0,90));     
        }
    }

}
