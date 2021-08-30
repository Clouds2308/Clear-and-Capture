using System;
using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{
    [SerializeField] private int _currPlayerHealth = 100;   //players health at the moment
    [SerializeField] private int _maxPlayerHealth = 100;    //max health player can have     
    [SerializeField] private Animator _canvasAnimator;

    public int CurrentHealth { get => _currPlayerHealth;  private set => _currPlayerHealth = value; }
    
    public void TakeDamage(float damage)
    {

        _currPlayerHealth -= (int) damage;
        _canvasAnimator.SetTrigger("IsPlayerDamage");

        if (_currPlayerHealth <= 0)
        {
            //GameEvents.current.PlayerDead();    //raise event when player dies
        }
    }   

    private void Start()
    {        
        CurrentHealth = _maxPlayerHealth;
    }

}
