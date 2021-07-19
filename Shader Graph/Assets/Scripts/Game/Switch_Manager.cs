using UnityEngine;
using UnityEngine.UI;

public class Switch_Manager : MonoBehaviour
{
    private GameObject _player;
    private GameObject _drone;
    private GameObject _gun;  
    [SerializeField] private Animator _switchFadeAnimator;
    [SerializeField] private KeyCode _switchButton;
   
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _playerCamera;

    [SerializeField] private DroneInput _droneInput;
    [SerializeField] private DroneMovement _droneMovement;
    [SerializeField] private DroneShoot _droneShoot;
    [SerializeField] private GameObject _droneCamera;

    private static bool _isDroning;
    public static bool IsDroning { get => _isDroning; private set => _isDroning = value; }    

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _player = GameObject.FindGameObjectWithTag("Player");
        _playerInput = _player.GetComponent<PlayerInput>();
        _playerMovement = _player.GetComponent<PlayerMovement>();
        _playerCamera = _player.transform.GetChild(0).gameObject;

        _drone = GameObject.FindGameObjectWithTag("Drone");
        _droneInput = _drone.GetComponent<DroneInput>();
        _droneMovement = _drone.GetComponent<DroneMovement>();
        _droneShoot = _drone.GetComponent<DroneShoot>();
        _droneCamera = _drone.transform.GetChild(0).gameObject;

        _switchFadeAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
        
    }

    private void Update()
    {
        ChangeState();        
    }

    void DeployDrone()
    {
        _playerInput.enabled = false;       //disable input script on player when droning
        _playerMovement.enabled = false;    //disable movement script on player when droning
        _playerCamera.SetActive(false);     //disable camera on player        
        _gun.GetComponent<Weapon>().enabled = false;                   //disable gunfire script on gun when droning

        _droneInput.enabled = true;         //enable input script on drone when droning
        _droneMovement.enabled = true;       //enable movement script on drone when droning
        _droneShoot.enabled = true;               //enable shoot script on drone when droning
        _drone.transform.GetChild(0).gameObject.SetActive(true);        //enable camera on drone

        if (!_drone.activeInHierarchy)
        {
            _drone.transform.position = _player.transform.GetChild(1).transform.position;   //set 1st drone location to dronespawner
            _drone.SetActive(true);                                     //enable drone
        }

    }

    void ExitDrone()
    {
        _playerInput.enabled = true;        //enable input script on player when not droning
        _playerMovement.enabled = true;     //enable movement script on player when not droning
        _playerCamera.SetActive(true);       //enable camera on player
        _gun.GetComponent<Weapon>().enabled = true;                   //enable gunfire script on gun when not droning

        _droneInput.enabled = false;        //disable input script on drone when not droning
        _droneMovement.enabled = false;     //disable movement script on drone when not droning
        _droneShoot.enabled = false;        //disable shoot script on drone when not droning
        _droneCamera.SetActive(false);      //disable camera on drone

    }

    void ChangeState()
    {
        if (Input.GetKeyDown(_switchButton))
        {
            _switchFadeAnimator.SetTrigger("IsSwitch");

            if (_isDroning != true)
            {
                Invoke("DeployDrone", 0.9f);
                _isDroning = true;
            }
            else if (_isDroning == true)
            {
                Invoke("ExitDrone", 0.9f);
                _isDroning = false;
            }
        }      
    }     
    
}