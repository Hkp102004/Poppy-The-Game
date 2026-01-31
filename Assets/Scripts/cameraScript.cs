using UnityEngine;

public class cameraScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;

    void Start()
    {
        
    }


    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x + offset.x ,offset.y,offset.z);
    }
}
