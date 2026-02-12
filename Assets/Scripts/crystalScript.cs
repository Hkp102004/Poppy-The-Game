using System;
using UnityEngine;
using UnityEngine.UIElements;

public class crystalScript : MonoBehaviour
{
    [TextArea]
    public String message;
    public UIManager uIManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            uIManager.ShowMessage(message);
            Destroy(gameObject);
        }
    }
}
