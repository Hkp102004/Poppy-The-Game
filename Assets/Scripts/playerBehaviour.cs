using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 10f;
    [SerializeField] private float height = 300f;
    private int count = 0;
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

        if(transform.position.x <= -9.3f)
        {
            transform.position = new Vector3(-9.3f, transform.position.y, transform.position.z);
        }

        if(Input.GetKeyDown(KeyCode.Space)) //jumpting 
        {
            Jump();
        }
    }

    public void Jump() //jump function 
    {
        if(count < 2)
        {
            transform.Translate(Vector3.up * height * Time.deltaTime);
            count++;
            StartCoroutine(JumpCooldown()); //cooldown for jump
        }
    }

    IEnumerator JumpCooldown() //cooldown for jump to wait 2 seconds
    {
        yield return new WaitForSeconds(2f);
        count = 0;
    }
}
