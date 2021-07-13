using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    [SerializeField] private DroneInput _inputScript;  //reference to drone input script
    [SerializeField] private Transform _groundCheck; //groundCheck on drone
    [SerializeField] private Transform _droneCamera;    //camera that sits on drone
    [SerializeField] private Rigidbody _droneRb;        //drone rigidbody

    [SerializeField] private float _cameraSensitivity = 5f;  //sensitivity of camera movement
    [SerializeField] private float _cameraMinimumY = -70f;   //minimum camera clamp   
    [SerializeField] private float _cameraMaximumY = 70f;    //maximum camera clamp
    [SerializeField] private float _rotationSmooth = 10f;    //smooth damp for camera rotation
    [SerializeField] private float _walkSpeed = 9f;          //drone walk speed;
    [SerializeField] private float _yJumpForce = 30f;        //jump force in y direction
    [SerializeField] private float _extraGravity = 20;       //additional gravity 
    [SerializeField] private bool _isGrounded;               // bool for checking if drone is grounded
    [SerializeField] private LayerMask _groundLayer;         //ground layermask   

    private float _bodyRotationX;    //rotation of body on x axis 
    private float _cameraRotationY;  //rotation of camera on y axis
    private Vector3 _directionalIntentX; 
    private Vector3 _directionalIntentZ;
    private bool _canJump;

    #region CameraControl
    void LookAround()
    {
        _bodyRotationX += _inputScript.MouseX * _cameraSensitivity;      //store mouse input on x axis 
        _cameraRotationY += _inputScript.MouseY * _cameraSensitivity;    //store mouse input on y  axis  

        //clamp camera rotation on y axis
        _cameraRotationY = Mathf.Clamp(_cameraRotationY, _cameraMinimumY, _cameraMaximumY);

        //create camera target rotations i.e; what value camera should rotate towards
        Quaternion camTargetRotation = Quaternion.Euler(-_cameraRotationY, 0, 0);    // target for mouse rotation on y axis
        Quaternion bodyTargetRotation = Quaternion.Euler(0, _bodyRotationX, 0);      //target for player rotation on x axis

        //handle actual rotations
        transform.rotation = Quaternion.Lerp(transform.rotation, bodyTargetRotation, Time.deltaTime * _rotationSmooth); // rotate body on x axis using lerp

        _droneCamera.localRotation = Quaternion.Lerp(_droneCamera.localRotation, camTargetRotation, Time.deltaTime * _rotationSmooth); // rotate camera on y axis using lerp

    }

    #endregion

    #region PlayerMovement
    void MovementControl()
    {
        //Direction must match camera direction
        _directionalIntentX = _droneCamera.right;
        _directionalIntentX.y = 0;
        _directionalIntentX.Normalize();

        _directionalIntentZ = _droneCamera.forward;
        _directionalIntentZ.y = 0;
        _directionalIntentZ.Normalize();

        _droneRb.velocity = _directionalIntentZ * _inputScript.InputZ * _walkSpeed + _directionalIntentX * _inputScript.InputX * _walkSpeed + Vector3.up * _droneRb.velocity.y; // actual motor work to move body using velocity            
    }

    #endregion

    void AdditionalGravity()
    {
        _droneRb.AddForce(Vector3.down * _extraGravity);
    }

    void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, 0.5f, _groundLayer);
    }

    void DroneJump()
    {
        _droneRb.AddForce(Vector3.up * _yJumpForce, ForceMode.Impulse);
    }

    private void Start()
    {
        _inputScript = GetComponent<DroneInput>();
    }
   

    private void Update()
    {
        LookAround();
        GroundCheck();

        if(_isGrounded && _inputScript.Jump)
        {
            _canJump = true;
        }
    }

    private void FixedUpdate()
    {        
        MovementControl();
        AdditionalGravity();

        if(_canJump == true)
        {
            DroneJump();
            _canJump = false;
        }
    }
}