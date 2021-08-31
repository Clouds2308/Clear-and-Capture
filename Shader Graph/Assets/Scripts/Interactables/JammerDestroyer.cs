using UnityEngine;

public class JammerDestroyer : MonoBehaviour, IDestructibleByGun
{
    [SerializeField] private GameObject explosionEffect;

    public void DestroyOnHit()
    {
        GameObject clone = (GameObject)Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        Destroy(clone, 2f);
    }
}
