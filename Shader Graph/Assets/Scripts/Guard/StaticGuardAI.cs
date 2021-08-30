using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StaticGuardAI : MonoBehaviour
{
    private Guard _guard;
    private Transform _player;
    private Animator _guardAnimator;

    [SerializeField] private Transform _raycaster;

    [Header("Values")]
    [SerializeField] private float _sightRange;
    [SerializeField] private float _captureRange;
    [SerializeField] private LayerMask _whatisPlayer;
    [Range(0,180)]
    [SerializeField] private float _guardFOV;

    private NavMeshAgent _guardAgent;
    private bool _playerinSightRange, _playerinCaptureRange,_playerinLos;   
    private RaycastHit _hitInfo;    
    private Vector3 _playerDir;
    private Vector3 _destination;    
        

    private void Start()
    {
        _guardAgent = GetComponent<NavMeshAgent>();
        _destination = _guardAgent.destination;

        _guardAnimator = GetComponent<Animator>();
        _player = FindObjectOfType<Player>().GetComponent<Transform>();

        _guard = GetComponent<Guard>();
    }

    private void Update()
    {           
        if (_playerinSightRange && !_playerinCaptureRange && _playerinLos)            
            Chase();
        else            
            StopAgent();
                
        if (_playerinSightRange && _playerinCaptureRange && _playerinLos)
            Capture();

        if (_guard.IsGuardDead == true)
        {
            _guardAgent.isStopped = true;
            this.enabled = false;
        }
                        
    }


    private void FixedUpdate()
    {
        LookForPlayer();
    }

    private void LookForPlayer()
    {
        _playerDir = _player.position - _raycaster.position;
        float guardAngle = Vector3.Angle(_playerDir, _raycaster.forward);

        if (Physics.Raycast(_raycaster.position, _playerDir, out _hitInfo, _whatisPlayer))       //check player in fov and los
        {
            Debug.DrawLine(_raycaster.position, _hitInfo.point, Color.green);
            if (_hitInfo.transform.CompareTag("Player") && (guardAngle >= -_guardFOV && guardAngle <= _guardFOV))
                _playerinLos = true;
            else
                _playerinLos = false;                            
        }

        _playerinSightRange = Physics.CheckSphere(transform.position, _sightRange, _whatisPlayer);  //check for player in sight range
        _playerinCaptureRange = Physics.CheckSphere(transform.position, _captureRange, _whatisPlayer);  //check for player in capture range

    }
    private void Chase()
    {
        _destination = _player.position;    //set destination to player position
        _guardAgent.destination = _destination; //set agent destination to destination

        _guardAnimator.SetBool("canWalk", true);
        _guardAnimator.SetBool("canIdle", false);

    }
    private void StopAgent()
    {
        _destination = _guardAgent.transform.position;  //set destination to current transform position
        _guardAgent.destination = _destination; //set agent destination to same position

        _guardAnimator.SetBool("canWalk", false);
        _guardAnimator.SetBool("canIdle", true);

    }
    private void Capture()
    {
        _guardAgent.isStopped = true;   //stop agent travel and play further animations    

        _guardAnimator.SetBool("canWalk", false);
        _guardAnimator.SetBool("canIdle", false);
        _guardAnimator.SetTrigger("isCapture");

        GameEvents.current.PlayerCapture(); // raise event when player is captured

    }    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _captureRange);
    }
    
}