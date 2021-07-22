using System;
using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{
    [SerializeField] private int _currPlayerHealth = 100;   //players health at the moment
    [SerializeField] private int _maxPlayerHealth = 100;    //max health player can have     

    public int CurrentHealth { get => _currPlayerHealth;  private set => _currPlayerHealth = value; }
    
    public void TakeDamage(float damage)
    {

        _currPlayerHealth -= (int) damage;
       // GameEvents.current.PlayerHealthChange(); //raise event when player heatlh changes


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
