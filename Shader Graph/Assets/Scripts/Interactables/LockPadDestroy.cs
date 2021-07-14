using UnityEngine;
using System.Collections;

public class LockPadDestroy : MonoBehaviour,IDestructibleByDrone
{
    [SerializeField] private GameObject plasmaEffect;
    public void DestroyOnHit()
    {
        GameObject clone = (GameObject) Instantiate(plasmaEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject,1f);
        Destroy(clone, 2f);
    }
        
}
