using UnityEngine;

public interface IDestructibleByGun
{
    void DestroyOnHit();    
}

public interface IDestructibleByDrone
{
    void DestroyOnHit(); 
}

public interface IDamageable
{
    void TakeDamage(float damage);
}