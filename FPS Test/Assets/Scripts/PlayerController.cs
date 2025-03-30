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
    
    private Vector2 _rotStore;
    
    public float lookSpeed;

    public Camera theCam;

    public float minViewAngle, maxViewAngle;

    public InputActionReference jumpAction;

    public float jumpPower, runJumpPower;

    public float gravityModifier = 4f;

    public float runSpeed;

    public InputActionReference sprintAction;

    public float camZoomNormal, camZoomOut, camZoomSpeed;

    public WeaponsController weaponCon;

    public InputActionReference shootAction;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        //Locks the cursor to the game window.
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float yStore = _currentMovement.y;
        
        //
        // Debug.Log(moveInput);
        //
        // Uses the moveInput vector 2 as a parameter for the input and direction
        //  Takes the moveInput and uses as a direction in the parameter 
        // _currentMovement = new Vector3(moveInput.x * moveSpeed, 0f, moveInput.y * moveSpeed);
        //
        
        //
        //Handling movement
        //
        
        //moveAction is the action in the new unity input system
        //reads a Vector 2 for movement
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        
        //Takes the move input y value and applies it to the transform forward 
        //This is a great work around for fixing the movement
        Vector3 moveForward = transform.forward * moveInput.y;
        
        //Takes the move input x value and applies it to the transform right to move left and right 
        Vector3 moveSideways = transform.right * moveInput.x;

        if (sprintAction.action.IsPressed())
        {
            _currentMovement = (moveForward + moveSideways) * runSpeed;

            if (_currentMovement != Vector3.zero)
            {
                 theCam.fieldOfView = Mathf.Lerp(theCam.fieldOfView, camZoomOut, camZoomSpeed * Time.deltaTime);
            }
        }
        else
        {
            //You have to add these two together because you can not multiply a vector 3 by a vector 3
            _currentMovement = (moveForward + moveSideways) * moveSpeed;
            theCam.fieldOfView = Mathf.Lerp(theCam.fieldOfView, camZoomNormal, camZoomSpeed * Time.deltaTime);

        }
    
        
        //
        // Handling gravity
        // checks to see if the player is jumping or on the ground to change the gravity and sets the gravity to 0
        //
        if (charCon.isGrounded == true)
        {
            yStore = 0f;
        }
        
        _currentMovement.y = yStore + (Physics.gravity.y * Time.deltaTime *gravityModifier);
        
        
        //
        //Handling jumping
        //

        if (jumpAction.action.WasPressedThisFrame() && charCon.isGrounded == true)
        {
            _currentMovement.y = jumpPower;
        }


        //
        //Handling looking and rotation
        //
        //Uses the character controller attached to the player to move
        charCon.Move(_currentMovement * Time.deltaTime);
        
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
        
        //this is for making the look not inverted on the y axis
        lookInput.y = -lookInput.y;

        _rotStore = _rotStore + (lookInput * lookSpeed * Time.deltaTime);

        _rotStore.y = Mathf.Clamp(_rotStore.y, minViewAngle, maxViewAngle);
        
        transform.rotation = Quaternion.Euler(0f, _rotStore.x, 0f);
        
        //takes the rotation
        theCam.transform.localRotation = Quaternion.Euler(_rotStore.y, 0f,0f );


        if (shootAction.action.WasPressedThisFrame())
        {
            weaponCon.Shoot();
        }

        if (shootAction.action.IsPressed())
        {
            weaponCon.ShootHold();
        }
        
    }
}
