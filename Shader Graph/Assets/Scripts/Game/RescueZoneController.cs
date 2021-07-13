using UnityEngine;

public class RescueZoneController : MonoBehaviour
{
    private bool _isHostageRescued;

    private void Start()
    {
        GameEvents.current.onHostageFree += UpdateRescueBool;
        _isHostageRescued = false;
    }

    private void UpdateRescueBool()
    {
        _isHostageRescued = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name == "Player" && _isHostageRescued == true)
        {
            GameEvents.current.RescueZoneEnter();
        }
    }
}
