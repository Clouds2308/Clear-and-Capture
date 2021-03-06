using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera _playerCam;
    public LayerMask _mask;

    [Header("Weapon")]
    [SerializeField] private Animator _handsAnimator;
    [SerializeField] private Animator _gunAnimator;
    [SerializeField] private float _weaponRange;    //weapon bullet hit range
    [SerializeField] private float _timeToReload;   //weapon reload time
    [SerializeField] private float _weaponDamage; //weapon base damage
    [SerializeField] private float _fireRate;  //firerate of weapon
    private float nextTimeToFire = 0f;
    private bool _canFire = true;
    public bool CanFire { get => _canFire; set => _canFire = value; }
    private bool _canReload = true;
    public bool CanReload { get => _canReload; set => _canReload = value; }

    [Header("Bullets")]
    [SerializeField] private int _bulletsInMag;    //bullets present in current magazine
    [SerializeField] private int _maxBulletsInMag; //max bullets in all magazine
    [SerializeField] private int _magCapacity;     //capacity of a magazine
    private bool _isMagEmpty;   // check bullets in current magazine
    private bool _isWeaponEmpty;    //check bullets total

    [Header("Effects")]
    public ParticleSystem BulletTracer;
    public ParticleSystem MuzzleFlash;
    public GameObject EnemyBloodEffect;
    public GameObject ImpactEffect;
    public AudioClip ShootAudio;
    public AudioClip CasingDropAudio;
    public AudioClip DryFireAudio;
    public AudioClip ReloadAudio;

    public int BulletInMag { get => _bulletsInMag; private set => _bulletsInMag = value; }
    public int MaxBulletsInMag { get => _maxBulletsInMag; private set => _maxBulletsInMag = value; }
    
    private void Update()
    {
        _isMagEmpty = (BulletInMag == 0);
        _isWeaponEmpty = (MaxBulletsInMag == 0);

        if (Input.GetKeyDown(KeyCode.R) && _isMagEmpty && !_isWeaponEmpty && _canReload)
        {
            StartCoroutine(ReloadWeapon());            
        }

        if (WeaponSwitch.SelectedWeapon == 0)
        {
            
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && _canFire)
            {
                if (!_isMagEmpty)
                {
                    nextTimeToFire = Time.time + 1f / _fireRate;
                    FireWeapon();
                }
                else
                    AudioManager.instance.PlaySound(DryFireAudio, transform.position);
            }
        }

        if (WeaponSwitch.SelectedWeapon == 1)
        {
           
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && _canFire)
            {
                if (!_isMagEmpty)
                {
                    nextTimeToFire = Time.time + 1f / _fireRate;
                    FireWeapon();
                }
                else
                    AudioManager.instance.PlaySound(DryFireAudio, transform.position);
            }
        }

        if (WeaponSwitch.SelectedWeapon == 2)
        {

            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && _canFire)
            {
                if (!_isMagEmpty)
                {
                    nextTimeToFire = Time.time + 1f / _fireRate;
                    FireWeapon();
                }
                else
                    AudioManager.instance.PlaySound(DryFireAudio, transform.position);
            }
        }

    }

    void FireWeapon()
    {

        _handsAnimator.SetTrigger("IsFire");
        _gunAnimator.SetTrigger("IsFire");

        MuzzleFlash.Play();
        BulletTracer.Play();
        AudioManager.instance.PlaySound(ShootAudio, transform.position);
        AudioManager.instance.PlaySound(CasingDropAudio, transform.position);

        BulletInMag -= 1;
        RaycastHit _hit;

        if (Physics.Raycast(_playerCam.transform.position, _playerCam.transform.forward, out _hit, _weaponRange,_mask))
        {
            IDamageable _guard = _hit.transform.GetComponent<IDamageable>();
            IDestructibleByGun _destructible = _hit.transform.GetComponent<IDestructibleByGun>();

            if (_guard != null)
            {
                _guard.TakeDamage(_weaponDamage);

                GameObject bloodGo = Instantiate(EnemyBloodEffect, _hit.point, Quaternion.LookRotation(_hit.normal));
                Destroy(bloodGo, 1f);
            }
                             
            if(_destructible!=null)
                _destructible.DestroyOnHit();

             GameObject impactGo = Instantiate(ImpactEffect, _hit.point, Quaternion.LookRotation(_hit.normal));
             Destroy(impactGo, 1f);
        }
    }   

    IEnumerator ReloadWeapon()
    {
        _handsAnimator.SetTrigger("IsReload");
        _gunAnimator.SetTrigger("IsReload");
        AudioManager.instance.PlaySound(ReloadAudio, transform.position);
        _canFire = false;
        _canReload = false;

        if(MaxBulletsInMag<=0)
        {
            MaxBulletsInMag = 0;
        }
                

        yield return new WaitForSeconds(_timeToReload);

        if (MaxBulletsInMag < _magCapacity)
        {
            BulletInMag += MaxBulletsInMag;
            MaxBulletsInMag -= MaxBulletsInMag;
        }
        else
        {
            BulletInMag += _magCapacity;
            MaxBulletsInMag -= _magCapacity;
        }


        _canFire = true;
        _canReload = true;
    }

    public void PickupAmmo(int amount)
    {
        _maxBulletsInMag += amount;
    }
       
}