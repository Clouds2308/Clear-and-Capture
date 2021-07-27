﻿using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int _bulletCount;
    private Weapon _weapon;

    [Header("Effects")]
    [SerializeField] private AudioClip AmmoPickupAudio;

    private void Start()
    {
        _weapon = FindObjectOfType<Weapon>().GetComponent<Weapon>();
        _bulletCount = transform.childCount * 5;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            _weapon.PickupAmmo(_bulletCount);

            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(false);
                AudioManager.instance.PlaySound(AmmoPickupAudio, transform.position);
            }
        }
    }
}
