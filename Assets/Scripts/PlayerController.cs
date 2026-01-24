using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private CharacterController controller;
    private Vector3 moveInput;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Movement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log($"The key you pressed is: {moveInput}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
