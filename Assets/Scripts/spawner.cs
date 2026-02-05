using Unity.VisualScripting;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] private GameObject rock; //the rock prefab that should be sopwaned again and again
    [SerializeField] private float spawnRate = 2f; //the rate at which the bopulders should be spawned

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnRate <= 0)
        {
            Instantiate(rock, new Vector3(transform.position.x, Random.Range(-0.9f,5f), transform.position.z), Quaternion.identity);
            spawnRate =2f;
        }
        else
        {
            spawnRate -= Time.deltaTime;
        }
    }
}
