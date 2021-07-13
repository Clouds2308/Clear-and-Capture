using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera _playerCam;

    [Header("Weapon")]
    [SerializeField] private Animator _handsAnimator;
    [SerializeField] private Animator _gunAnimator;
    [SerializeField] private float _weaponRange;    //weapon bullet hit range
    [SerializeField] private float _timeToReload;   //weapon reload time
    [SerializeField] private float _weaponDamage = 10f; //weapon base damage
    private float _fireRate = 10f;  //firerate of weapon
    private float nextTimeToFire = 0f;
    private bool _canFire = true;

    [Header("Bullets")]
    [SerializeField] private int _bulletsInMag = 10;    //bullets present in current magazine
    [SerializeField] private int _maxBulletsInMag = 30; //max bullets in all magazine
    [SerializeField] private int _magCapacity = 10;     //capacity of a magazine
    private bool _isMagEmpty;
    private bool _isWeaponEmpty;

    public int BulletInMag { get => _bulletsInMag; private set => _bulletsInMag = value; }
    public int MaxBulletsInMag { get => _maxBulletsInMag; private set => _maxBulletsInMag = value; }

    // public ParticleSystem muzzleFlash;
    //public GameObject impactEffect;
    
    private void Update()
    {
        _isMagEmpty = (BulletInMag == 0);
        _isWeaponEmpty = (MaxBulletsInMag == 0);

        if (Input.GetKeyDown(KeyCode.R) && _isMagEmpty && !_isWeaponEmpty)
        {
            StartCoroutine(ReloadWeapon());            
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && !_isMagEmpty &&_canFire)
        {
            nextTimeToFire = Time.time + 1f/_fireRate;
            FireWeapon();
        }               
    }

    void FireWeapon()
    {
        _handsAnimator.SetTrigger("IsFire");
        _gunAnimator.SetTrigger("IsFire");

        BulletInMag -= 1;
        RaycastHit _hit;

        if (Physics.Raycast(_playerCam.transform.position, _playerCam.transform.forward, out _hit, _weaponRange))
        {
            IDamageable _guard = _hit.transform.GetComponent<IDamageable>();
            IDestructibleByGun _destructible = _hit.transform.GetComponent<IDestructibleByGun>();

            if(_guard!=null)
                _guard.TakeDamage(_weaponDamage);                
                             
            if(_destructible!=null)
                _destructible.DestroyOnHit();
                                    
            //GameObject impactGo = Instantiate(impactEffect, _hit.point, Quaternion.LookRotation(_hit.normal));
            //Destroy(impactGo, 1f);
        }
    }   

    IEnumerator ReloadWeapon()
    {
        _handsAnimator.SetTrigger("IsReload");
        _gunAnimator.SetTrigger("IsReload");
        _canFire = false;

        if(MaxBulletsInMag<=0)
        {
            MaxBulletsInMag = 0;
            Debug.Log("Weapon Empty");
        }    

        yield return new WaitForSeconds(_timeToReload);

        MaxBulletsInMag -= _magCapacity;
        BulletInMag += _magCapacity;
        _canFire = true;
    }
       
}