using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class MovingEnemyAI : MonoBehaviour
{
    private Transform _player;
    private Animator _thugAnimator;

    [Header("Patorl Points")]
    [SerializeField] private Transform[] _patrolPoints;
    private int _destPoint = 0;

    [Header("Values")]
    [SerializeField] private float _sightRange;
    [SerializeField] private LayerMask _whatisPlayer;
    [Range(0, 180)]
    [SerializeField] private float _guardFOV;

    [Header("Weapon")]
    [SerializeField] private float _weaponDamage = 5f;    
    [SerializeField] private float _fireRate;
    private float nextTimetoFire = 0f;
    [Range(0.0f,1.0f)]
    [SerializeField] private float _hitAccuracy = 0.5f;
    [SerializeField] private AudioSource test;
    [SerializeField] private AudioClip _weaponShootAudio;

    private NavMeshAgent _thugAgent;
    private bool _playerinSightRange, _playerinLos;
    private RaycastHit _hitInfo;
    private Vector3 _playerDir;
    private Vector3 _destination;


    private void Start()
    {
        _thugAgent = GetComponent<NavMeshAgent>();
        _destination = _patrolPoints[_destPoint].position;

        _thugAnimator = GetComponent<Animator>();
        _player = FindObjectOfType<Player>().GetComponent<Transform>();

        Patrol();
    }

    private void Update()
    {
        if (_playerinSightRange && _playerinLos)
        {
            ChaseShootPlayer();
        }
        else
        {
            _thugAnimator.SetBool("CanShoot", false);
            Patrol();
        }

        if (Guard.IsGuardDead == true)
        {
            _thugAgent.isStopped = true;
            this.enabled = false;
        }

    }

    private void FixedUpdate()
    {
        LookForPlayer();
    }

    private void Patrol()
    {
        if (!_thugAgent.pathPending && _thugAgent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
    }

    private void GotoNextPoint()
    {
        if (_patrolPoints.Length == 0)
            return;

        _destination = _patrolPoints[_destPoint].position;
        _thugAgent.destination = _destination;

        _destPoint = (_destPoint + 1) % _patrolPoints.Length;
    }

    private void ChaseShootPlayer()
    {
        _destination = _player.position;
        _thugAgent.destination = _destination;

        _thugAnimator.SetBool("CanShoot",true);

        if(Time.time>=nextTimetoFire && !Guard.IsGuardDead)
        {
            nextTimetoFire = Time.time + 1f / _fireRate;
            StartCoroutine(ShootPlayer());           
        }
    }

    IEnumerator ShootPlayer()
    {
        yield return new WaitForSeconds(1.5f);

        float random = Random.Range(0.0f, 1.0f);
        if(random > 1f - _hitAccuracy)
        {
            _player.GetComponent<Player>().TakeDamage(_weaponDamage);
        }
        test.PlayOneShot(_weaponShootAudio);
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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _sightRange);
    }  

}