using UnityEngine;
using UnityEngine.UI;

public class Switch_Manager : MonoBehaviour
{
    private GameObject _player;
    private GameObject _drone;
    private Animator _switchFadeAnimator;

    [SerializeField] private Weapon _weapon;  
    [SerializeField] private KeyCode _switchButton;

    [Header("Effects")]
    public AudioClip DroneOutAudio;
    public AudioClip DroneInAudio;
   
    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;
    private GameObject _playerCamera;

    private DroneInput _droneInput;
    private DroneMovement _droneMovement;
    private DroneShoot _droneShoot;
    private GameObject _droneCamera;

    private static bool _isDroning;
    public static bool IsDroning { get => _isDroning; private set => _isDroning = value; }    

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerInput = _player.GetComponent<PlayerInput>();
        _playerMovement = _player.GetComponent<PlayerMovement>();
        _playerCamera = _player.transform.GetChild(0).gameObject;

        _drone = GameObject.FindGameObjectWithTag("Drone");
        _droneInput = _drone.GetComponent<DroneInput>();
        _droneMovement = _drone.GetComponent<DroneMovement>();
        _droneShoot = _drone.GetComponent<DroneShoot>();
        _droneCamera = _drone.transform.GetChild(0).gameObject;

        _weapon = FindObjectOfType<Weapon>().GetComponent<Weapon>();

        _switchFadeAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
        
    }

    void DeployDrone()
    {
        
        _playerInput.enabled = false;       //disable input script on player when droning
        _playerMovement.enabled = false;    //disable movement script on player when droning
        _playerCamera.SetActive(false);     //disable camera on player        
        _playerCamera.GetComponent<AudioListener>().enabled = false;    //mute sounds for player when droning
        _weapon.enabled = false;                   //disable gunfire script on gun when droning

        _droneInput.enabled = true;         //enable input script on drone when droning
        _droneMovement.enabled = true;       //enable movement script on drone when droning
        _droneShoot.enabled = true;               //enable shoot script on drone when droning
        _droneCamera.SetActive(true);       //enable camera on drone
        _droneCamera.GetComponent<AudioListener>().enabled = true;  //enable audio for drone

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
        _playerCamera.GetComponent<AudioListener>().enabled = true;    //enable sounds for player when not droning
        _weapon.enabled = true;                   //enable gunfire script on gun when not droning        

        _droneInput.enabled = false;        //disable input script on drone when not droning
        _droneMovement.enabled = false;     //disable movement script on drone when not droning
        _droneShoot.enabled = false;        //disable shoot script on drone when not droning
        _droneCamera.SetActive(false);      //disable camera on drone
        _droneCamera.GetComponent<AudioListener>().enabled = false;  //disable audio for drone

    }

    void ChangeState()
    {
        if (Input.GetKeyDown(_switchButton))
        {
            _switchFadeAnimator.SetTrigger("IsSwitch");

            if (_isDroning != true)
            {
                AudioManager.instance.PlaySound(DroneOutAudio, transform.position);
                Invoke("DeployDrone", 0.9f);
                _isDroning = true;
            }
            else if (_isDroning == true)
            {
                AudioManager.instance.PlaySound(DroneInAudio, transform.position);
                Invoke("ExitDrone", 0.9f);
                _isDroning = false;
            }
        }      
    }     

    private void Update()
    {
        ChangeState();        
    }
    
}