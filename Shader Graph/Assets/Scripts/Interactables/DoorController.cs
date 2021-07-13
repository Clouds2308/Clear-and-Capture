using UnityEngine;
using System;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject _doorwayPanel;
    [SerializeField] private Animator _doorAnimator;
    [SerializeField] private int _id;

    private void Start()
    {
        GameEvents.current.onDoorwayTriggerEnter += onDoorwayOpen;
    }  

    private void OnTriggerExit(Collider other)
    {
        _doorwayPanel.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.name == "Drone")
        {
            _doorwayPanel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && transform.childCount == 3)
            {                
                GameEvents.current.DoorwayTriggerEnter(_id);                
            }
        }
    }

    private void onDoorwayOpen(int id)
    {
        if (id == this._id)
        {            
            _doorAnimator.SetTrigger("IsOpen");                       
        }    
    }

    private void OnDisable()
    {
        GameEvents.current.onDoorwayTriggerEnter -= onDoorwayOpen;        
    }    
}
