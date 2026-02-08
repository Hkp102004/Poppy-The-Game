using UnityEngine;

public class cameraScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;

    void Start()
    {
        if(player == null)
        {
            Debug.LogError("Player transform is missing in camera script");
            return;
        }
    }


    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x + offset.x ,player.position.y +offset.y,offset.z);
    }
}
    