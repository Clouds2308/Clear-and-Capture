using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StaticGuardAI : MonoBehaviour
{
    private Transform _player;
    private Animator _guardAnimator;

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
    }

    private void Update()
    {           
        if (_playerinSightRange && !_playerinCaptureRange && _playerinLos)            
            Chase();
        else            
            StopAgent();
                
        if (_playerinSightRange && _playerinCaptureRange && _playerinLos)
            Capture();

        if (Guard.IsGuardDead == true)
            _guardAgent.isStopped = true;
                        
    }


    private void FixedUpdate()
    {
        LookForPlayer();
    }

    private void LookForPlayer()
    {
        _playerDir = _player.position - transform.position;
        float guardAngle = Vector3.Angle(_playerDir, transform.forward);

        if (Physics.Raycast(transform.position, _playerDir, out _hitInfo, _whatisPlayer))       //check player in fov and los
        {
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