using Unity.VisualScripting;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] private GameObject rock; //the rock prefab that should be sopwaned again and again
    [SerializeField] private float spawnRate = 2f; //the rate at which the bopulders should be spawned
    [SerializeField] private Transform player_position;
    [SerializeField] private bool active;
    playerBehaviour playerScript;


    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerBehaviour>();
        
        if(playerScript == null)
        {
            Debug.LogError("Player Script is missing in spawner script");
        }
        if(rock == null)
        {
            Debug.LogError("rock is missing in the spawner script");
            return;
        }
        if(player_position == null)
        {
            Debug.LogError("Player position is missing in the spawner script");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player_position.position.x + 17, transform.position.y, transform.position.z);

        if(spawnRate <= 0 && active)
        {
            Instantiate(rock, new Vector3(transform.position.x, Random.Range(-0.9f,5f), transform.position.z), Quaternion.identity);
            spawnRate =2f;
        }
        else
        {
            spawnRate -= Time.deltaTime;
        }

        if(playerScript.lives <= 0)
        {
            active = false;
        }
    }
}
