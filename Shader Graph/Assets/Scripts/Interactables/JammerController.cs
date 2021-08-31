using UnityEngine;

public class JammerController : MonoBehaviour,IDestructibleByGun
{
    
    private DroneShoot _droneShoot;   

    private void Start()
    {        
        _droneShoot = GameObject.FindGameObjectWithTag("Drone").GetComponent<DroneShoot>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Drone")
        {
            _droneShoot.canShoot = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.name == "Drone")
        {
            _droneShoot.canShoot = true;
        }
    }       

    public void DestroyOnHit()
    {        
        _droneShoot.canShoot = true;           
    }
}
