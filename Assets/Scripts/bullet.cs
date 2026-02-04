using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 15f; //the speed of the bullet
    [SerializeField] private float deathzone = 18; //the distance after which the bullet should be destroyed
    [SerializeField] private Transform player; //refernce of player position

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if(player==null)
        {
            Debug.LogError("Transform of player is missing in bullet script");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime); //movement of bullet

        if(transform.position.x > player.position.x + deathzone) //to destroy the game object
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Boulder")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
