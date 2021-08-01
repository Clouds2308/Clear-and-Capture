using UnityEngine;
using System;

public class ClaymoreController : MonoBehaviour, IDestructibleByGun
{
    private Player _player;
    [SerializeField] private Animator _damageScreenAnimator;
    [SerializeField] private float _claymoreExplosionDamage;

    [Header("Effects")]
    [SerializeField] private GameObject destroyEffecet;
    public AudioClip ExplosionAudio;

    public void DestroyOnHit()
    {
        GameObject clone = Instantiate(destroyEffecet, transform.position, Quaternion.identity) as GameObject;
        AudioManager.instance.PlaySound(ExplosionAudio, transform.position);
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
        _damageScreenAnimator.SetTrigger("IsPlayerDamage");
        _player.TakeDamage(_claymoreExplosionDamage);        
        DestroyOnHit();
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _damageScreenAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
    }

}
