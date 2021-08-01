using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private PlayerInput _inputScript;  //reference to player input script
    [SerializeField] private Transform _playerCamera;   //camera on player
    [SerializeField] private Rigidbody _playerRb;


    [SerializeField] private float _cameraSensitivity = 5f;  //sensitivity of camera movement
    public float CameraSensitivity { get => _cameraSensitivity; set => _cameraSensitivity = value; }
    [SerializeField] private float _cameraMinimumY = -70f;   //minimum camera clamp   
    [SerializeField] private float _cameraMaximumY = 70f;    //maximum camera clamp
    [SerializeField] private float _rotationSmooth = 10f;    //smooth damp for camera rotation
    [SerializeField] private float _speed = 9f;          //player walk speed;
    [SerializeField] private float _extraGravity = 20;       //additional gravity 
    [SerializeField] private bool _isGrounded;               // bool for checking if player is grounded
    [SerializeField] private LayerMask _groundLayer;         //ground layermask

    private float _bodyRotationX;    //rotation of body on x axis 
    private float _cameraRotationY;  //rotation of camera on y axis
    private Vector3 _directionalIntentX; 
    private Vector3 _directionalIntentZ;
    private bool IsWalking = false;

    [Header("Effects")]
    [SerializeField] public List<AudioClip> FootStepAudio = new List<AudioClip>();    

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

        _playerCamera.localRotation = Quaternion.Lerp(_playerCamera.localRotation, camTargetRotation, Time.deltaTime * _rotationSmooth); // rotate camera on y axis using lerp

    }

    #endregion

    #region PlayerMovement
    void MovementControl()
    {
        //Direction must match camera direction
        _directionalIntentX = _playerCamera.right;
        _directionalIntentX.y = 0;
        _directionalIntentX.Normalize();

        _directionalIntentZ = _playerCamera.forward;
        _directionalIntentZ.y = 0;
        _directionalIntentZ.Normalize();

        _playerRb.velocity = _directionalIntentZ * _inputScript.InputZ * _speed + _directionalIntentX * _inputScript.InputX * _speed + Vector3.up * _playerRb.velocity.y; // actual motor work to move body using velocity            
    }

    #endregion

    void AdditionalGravity()
    {
        _playerRb.AddForce(Vector3.down * _extraGravity);
    }   

    private void Start()
    {
        _inputScript = GetComponent<PlayerInput>();
        _playerRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {        

        if (_inputScript.InputX != 0 || _inputScript.InputZ != 0)
            if(!IsWalking)
            StartCoroutine(PlayFootStepSound(0.6f));
    }

    private void FixedUpdate()
    {
        MovementControl();
        AdditionalGravity();
    }

    private void LateUpdate()
    {
        LookAround();
    }

    IEnumerator PlayFootStepSound(float timer)
    {
        var randomIndex = Random.Range(0, FootStepAudio.Count);
        AudioManager.instance.PlaySound(FootStepAudio[randomIndex], transform.position);
        IsWalking = true;

        yield return new WaitForSeconds(timer);

        IsWalking = false;

    }
}
