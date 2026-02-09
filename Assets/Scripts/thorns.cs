using Unity.VisualScripting;
using UnityEngine;

public class thorns : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject player;
    [SerializeField] private float distance = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            player.transform.position = new  Vector3(player.transform.position.x - distance, player.transform.position.y + 3, player.transform.position.z);
        }
    }
}
