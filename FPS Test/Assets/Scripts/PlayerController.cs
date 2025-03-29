using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Attached to the player
    //Controlling the character
    //uses the character controller attached to the player parent
    public CharacterController charCon;
    
    public float moveSpeed;

    //uses the Player/Move in the InputSystems Manager
    public InputActionReference moveAction;

    private Vector3 _currentMovement;

    public InputActionReference lookAction;
    
    private Vector2 rotStore;
    
    public float lookSpeed;

    public Camera theCam;

    public float minViewAngle, maxViewAngle;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(moveInput);
        
        //Uses the moveInput vector 2 as a parameter for the input and direction
        // Takes the moveInput and uses as a direction in the parameter 
        //_currentMovement = new Vector3(moveInput.x * moveSpeed, 0f, moveInput.y * moveSpeed);
        
        //moveAction is the action in the new unity input system
        // reads a Vector 2 for movement
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();

        
        //Takes the move input y value and applies it to the transform forward 
        //This is a great work around for fixing the movement
        Vector3 moveForward = transform.forward * moveInput.y;
        
        //Takes the move input x value and applies it to the transform right to move left and right 
 
        Vector3 moveSideways = transform.right * moveInput.x;
    
        //You have to add these two together because you can not multiply a vector 3 by a vector 3
        _currentMovement = (moveForward + moveSideways) * moveSpeed;
        
        //
        charCon.Move(_currentMovement * Time.deltaTime);
        
        
        //Handling looking and rotation
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
        
        //this is for making the look not inverted on the y axis
        lookInput.y = -lookInput.y;

        rotStore = rotStore + (lookInput * lookSpeed * Time.deltaTime);

        rotStore.y = Mathf.Clamp(rotStore.y, minViewAngle, maxViewAngle);
        
        transform.rotation = Quaternion.Euler(0f, rotStore.x, 0f);
        
        
        
        //takes the rotation
        theCam.transform.localRotation = Quaternion.Euler(rotStore.y, 0f,0f );
        
    }
}
