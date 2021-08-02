using UnityEngine;
using UnityEngine.AI;

public class PatrolSwat : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    private int _destPoint;
    private NavMeshAgent _swatAgent;

    private void Start()
    {
        _swatAgent = GetComponent<NavMeshAgent>();

        GotoNextPoint();
    }

    private void GotoNextPoint()
    {
        if (_points.Length == 0)
            return;

        _swatAgent.destination = _points[_destPoint].position;

        _destPoint = (_destPoint + 1) % _points.Length;
    }

    private void Update()
    {
        if(!_swatAgent.pathPending && _swatAgent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
    }
}
