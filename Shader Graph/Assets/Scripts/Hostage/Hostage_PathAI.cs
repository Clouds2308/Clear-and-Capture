using UnityEngine;
using UnityEngine.AI;

public class Hostage_PathAI : MonoBehaviour
{
    [SerializeField] float stopThreshold;

    private Transform  _playerPos;
    private Animator _hostageAnimator;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _hostageAnimator = GetComponent<Animator>();
        _playerPos = FindObjectOfType<Player>().GetComponent<Transform>();
    }

    private void Update()
    {

        _hostageAnimator.SetBool("canWalk", true);
        agent.destination = _playerPos.position;


        //hostage traverse
        if (agent.remainingDistance < stopThreshold)
        {            
            _hostageAnimator.SetBool("canWalk", false);
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
                                    
    }
    
}
