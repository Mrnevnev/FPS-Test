using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    public CharacterController charCon;

    public float moveSpeed;

    public InputActionReference moveAction;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        
        charCon.Move(new Vector3(0f, 0f, moveSpeed) * Time.deltaTime);
    }
}
