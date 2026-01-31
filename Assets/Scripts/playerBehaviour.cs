using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float height = 400f;
    //[SerializeField] private bool Jumpie = true;
    private int jumpcount = 0;
    private int maxjump = 2;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
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

}
