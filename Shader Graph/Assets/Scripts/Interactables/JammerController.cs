using UnityEngine;

public class JammerController : MonoBehaviour,IDestructibleByGun
{
    [SerializeField] private DroneShoot _droneShoot;   
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private int _id;

    private void Start()
    {        
        GameEvents.current.onJammerTriggerEnter += OnJammerEnter;
        GameEvents.current.onJammerTriggeExit += OnJammerExit;
        _droneShoot = GameObject.FindGameObjectWithTag("Drone").GetComponent<DroneShoot>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Drone")
        {
            GameEvents.current.JammerTriggerEnter(_id);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.name == "Drone")
        {
            GameEvents.current.JammerTriggerExit(_id);
        }
    }

    void OnJammerEnter(int id)
    {
        if (id == this._id)
        {            
            _droneShoot.canShoot = false;
        }        
    }

    void OnJammerExit(int id)
    {
        if(id == this._id)
        {            
            _droneShoot.canShoot = true;
        }
    }

    private void OnDestroy()
    {
        GameEvents.current.onJammerTriggerEnter -= OnJammerEnter;
    }

    public void DestroyOnHit()
    {        
        _droneShoot.canShoot = true;
        GameObject clone = (GameObject)Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        Destroy(clone, 2f);
    }
}
