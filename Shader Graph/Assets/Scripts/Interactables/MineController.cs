﻿using UnityEngine;

public class MineController : MonoBehaviour,IDestructibleByDrone
{
    public GameObject explosionEffect;

    //[SerializeField] private Animator _damageScreenAnimator;
    private GameObject _droneCamera;
    private Player _player;

    [SerializeField] private int id;
    [SerializeField] private float _mineExplosionDamage = 10;


    private void OnCollisionEnter(Collision collision)
    {   
        if(collision.transform.name == "Drone")
        {
            _droneCamera.GetComponent<CameraEffects>().Shake();
        }
        if(collision.transform.name == "Player")
        {
            //_damageScreenAnimator.SetTrigger("isPlayerDamage");
            _player.TakeDamage(_mineExplosionDamage);
        }

        GameEvents.current.MineTriggerEnter(id);                      
    }
        
    private void OnMineEnter(int id)
    {
        if (id == this.id)
        {                                               
            DestroyOnHit();
        }
    }
    public void DestroyOnHit()
    {
        GameObject clone  = Instantiate(explosionEffect, transform.position, Quaternion.identity) as GameObject;
        Destroy(this.gameObject);
        Destroy(clone, 2f);
    }    

    private void OnDestroy()
    {
        GameEvents.current.onMineTriggerEnter -= OnMineEnter;
    }

    
    private void Start()
    {
        GameEvents.current.onMineTriggerEnter += OnMineEnter;
        _player = FindObjectOfType<Player>();
        _droneCamera = GameObject.FindGameObjectWithTag("Drone").transform.GetChild(0).gameObject;
    }
}