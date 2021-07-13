using UnityEngine;

public class JammerController : MonoBehaviour,IDestructibleByGun
{
    [SerializeField] private DroneShoot _manager;   
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private int _id;

    private void Start()
    {        
        GameEvents.current.onJammerTriggerEnter += OnJammerEnter;
        GameEvents.current.onJammerTriggeExit += OnJammerExit;
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
            _manager.canShoot = false;
        }        
    }

    void OnJammerExit(int id)
    {
        if(id == this._id)
        {            
            _manager.canShoot = true;
        }
    }

    private void OnDestroy()
    {
        GameEvents.current.onJammerTriggerEnter -= OnJammerEnter;
    }

    public void DestroyOnHit()
    {        
        _manager.canShoot = true;
        GameObject clone = (GameObject)Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        Destroy(clone, 2f);
    }
}
