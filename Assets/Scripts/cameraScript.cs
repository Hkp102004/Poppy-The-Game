//using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UIElements;

public class cameraScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed;

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
        // transform.position = new Vector3(player.position.x + offset.x ,player.position.y +offset.y,offset.z);

        // if(player == null)
        // {
        //     transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        // }
        if (player == null) return;

        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, offset.z);
    }
}
    