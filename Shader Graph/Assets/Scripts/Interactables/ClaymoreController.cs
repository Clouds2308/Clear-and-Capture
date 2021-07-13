using UnityEngine;
using System;

public class ClaymoreController : MonoBehaviour, IDestructibleByGun
{
    private Player _player;
    [SerializeField] private Animator _damageScreenAnimator;
    [SerializeField] private GameObject destroyEffecet;
    [SerializeField] private float _claymoreExplosionDamage;

    public void DestroyOnHit()
    {        
        GameObject clone = (GameObject) Instantiate(destroyEffecet, transform.position, Quaternion.identity);
        Destroy(this.gameObject,0.1f);
        Destroy(clone, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {        
        if(collision.transform.tag == "Player")
        {
            PlayerEnterClaymore();
        }
    }

    void PlayerEnterClaymore()
    {
        _damageScreenAnimator.SetTrigger("isPlayerDamage");
        _player.TakeDamage(_claymoreExplosionDamage);        
        DestroyOnHit();
    }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

}
