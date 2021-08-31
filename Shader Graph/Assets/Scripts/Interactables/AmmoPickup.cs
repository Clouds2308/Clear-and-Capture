using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int _bulletCount;
    private WeaponSwitch _weaponSwitch;
    private bool _ammoPickedup = false;

    [Header("Effects")]
    [SerializeField] private AudioClip AmmoPickupAudio;

    private void Start()
    {
        _weaponSwitch = FindObjectOfType<WeaponSwitch>().GetComponent<WeaponSwitch>();
        _bulletCount = transform.childCount * 5;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            _ammoPickedup = true;

           if(WeaponSwitch.SelectedWeapon == 0)
            {
                _weaponSwitch._weapon[0].PickupAmmo(_bulletCount);                
            }

           if(WeaponSwitch.SelectedWeapon == 1)
            {
                _weaponSwitch._weapon[1].PickupAmmo(_bulletCount);
            }

           if(WeaponSwitch.SelectedWeapon == 2)
            {
                _weaponSwitch._weapon[2].PickupAmmo(_bulletCount);
            }

            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(false);
                AudioManager.instance.PlaySound(AmmoPickupAudio, transform.position);                
            }

        }
    }

    private void Update()
    {
        if(_ammoPickedup == true)
        {
            _bulletCount = 0;
            AmmoPickupAudio = null;
        }
    }
}
