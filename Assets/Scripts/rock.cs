using Unity.VisualScripting;
using UnityEngine;

public class rock : MonoBehaviour
{
    [SerializeField]private Transform player;
    [SerializeField] private float speed = 15f;
    [SerializeField] private float deathzone = 18f;
    playerBehaviour playerScript;
    CircleCollider2D rockCollider;
    [SerializeField] private Animator explosion;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerBehaviour>();
        rockCollider = GetComponent<CircleCollider2D>();
        explosion = GetComponent<Animator>();
        if(player == null)
        {
            Debug.LogError("Tranform of player is missing in rock script");
            return;
        }
        if(playerScript == null)
        {
            Debug.LogError("PlayerBehaviour script is missing in rock script");
            return;
        }
        if(rockCollider == null)
        {
            Debug.LogError("the circle collider is missing in rock script");
            return;
        }
        if(explosion == null)
        {
            Debug.LogError("rock explosion animtion is missinf in rock script");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if(transform.position.x <= player.position.x - deathzone)
        {
            Destroy(gameObject);
        }
        Physics2D.IgnoreLayerCollision(6,7);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerScript.Damage();
            speed = 0;
            rockCollider.enabled = false;
            explosion.SetTrigger("blast");
            Destroy(gameObject,1.2f);
        }
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            speed = 0;
            rockCollider.enabled = false;
            explosion.SetTrigger("blast");
            Destroy(gameObject,1.2f);
        }
        if(collision.gameObject.tag == "Ground")
        {
            speed =0;
            explosion.SetTrigger("blast");
            Destroy(gameObject,1.2f);
        }
    }
}
