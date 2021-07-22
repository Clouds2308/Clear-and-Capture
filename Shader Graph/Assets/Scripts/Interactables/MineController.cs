using UnityEngine;

public class MineController : MonoBehaviour,IDestructibleByDrone
{
    public GameObject explosionEffect;

    [SerializeField] private Animator _damageScreenAnimator;
    [SerializeField] private float _mineExplosionDamage = 10;
    private GameObject _droneCamera;
    private Player _player;

    public AudioClip ExplosionAudio;


    private void OnCollisionEnter(Collision collision)
    {
        DestroyOnHit();

        if(collision.transform.name == "Drone")
        {
            _droneCamera.GetComponent<CameraEffects>().Shake();
        }
        if(collision.transform.name == "Player")
        {
            _damageScreenAnimator.SetTrigger("IsPlayerDamage");
            _player.TakeDamage(_mineExplosionDamage);
        }
    }
           
    public void DestroyOnHit()
    {
        GameObject clone  = Instantiate(explosionEffect, transform.position, Quaternion.identity) as GameObject;
        AudioManager.instance.PlaySound(ExplosionAudio, transform.position);
        Destroy(this.gameObject);
        Destroy(clone, 2f);
    }    
    
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _droneCamera = GameObject.FindGameObjectWithTag("Drone").transform.GetChild(0).gameObject;
        _damageScreenAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
    }
}