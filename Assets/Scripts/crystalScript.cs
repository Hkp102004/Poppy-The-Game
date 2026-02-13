using System;
using UnityEngine;
using UnityEngine.UIElements;

public class crystalScript : MonoBehaviour
{
    [TextArea]
    public String message;
    [SerializeField] private UIManager uIManager;
    void Start()
    {
        if(uIManager==null)
        {
            Debug.LogError("UI manager is missing in one of the crystal");
            return;
        }
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
