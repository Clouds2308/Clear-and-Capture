using System;
using UnityEngine;

public class DroneShoot : MonoBehaviour
{

    [SerializeField] private Transform _droneCamera;
    [SerializeField] private float shootRange = 20f;
    [SerializeField] private Transform _Emitter;
    [SerializeField] private LineRenderer _lr;

    [Header("Effects")]
    public AudioClip LaserFireAudio;

    public bool canShoot;

    private void Start()
    {
        canShoot = true;
        _lr.enabled = false;
    }    

    private void Update()
    {        
        if (Input.GetButtonDown("Fire1") && (Switch_Manager.IsDroning == true && canShoot==true))
            FireLine();       
    }

     private void FireLine()
     {
        AudioManager.instance.PlaySound(LaserFireAudio, transform.position);

        RaycastHit hitInfo;
        if (Physics.Raycast(_droneCamera.transform.position, _droneCamera.transform.forward, out hitInfo, shootRange))
        {
            _lr.enabled = true;
            _lr.SetPosition(0, _Emitter.transform.position);
            _lr.SetPosition(1, hitInfo.point);

            Invoke("TurnOffLaser", .5f);

            IDestructibleByDrone _destructible = hitInfo.transform.GetComponent<IDestructibleByDrone>();
            
            if(_destructible!=null)
            {                                
                _destructible.DestroyOnHit();
            }                        
        }

     }   

    void TurnOffLaser()
    {
        _lr.enabled = false;
    }   
}