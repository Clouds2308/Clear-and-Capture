using UnityEngine;
using TMPro;

public class WeaponSwitch : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _m1911UIPanel;
    [SerializeField] private GameObject _f1UIPanel;
    [SerializeField] private GameObject _mp5Panel;
    [SerializeField] private TextMeshProUGUI _currBulletsText;
    [SerializeField] private TextMeshProUGUI _maxBulletsText;

    public Weapon[] _weapon;
    public static int SelectedWeapon = 0;

    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        int previousselectedweapon = SelectedWeapon;

        if (SelectedWeapon == 0)
        {
            _currBulletsText.text = _weapon[0].BulletInMag.ToString();
            _maxBulletsText.text = _weapon[0].MaxBulletsInMag.ToString();
            _weapon[0].CanFire = true;
            _weapon[0].CanReload = true;
        }

        if (SelectedWeapon == 1)
        {
            _currBulletsText.text = _weapon[1].BulletInMag.ToString();
            _maxBulletsText.text = _weapon[1].MaxBulletsInMag.ToString();
            _weapon[1].CanFire = true;
            _weapon[1].CanReload = true;
        }

        if(SelectedWeapon == 2)
        {
            _currBulletsText.text = _weapon[2].BulletInMag.ToString();
            _maxBulletsText.text = _weapon[2].MaxBulletsInMag.ToString();
            _weapon[2].CanFire = true;
            _weapon[2].CanReload = true;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectedWeapon = 0;
            _f1UIPanel.SetActive(false);
            _mp5Panel.SetActive(false);
            _m1911UIPanel.SetActive(true);

        }

        if(Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >=2)
        {
            SelectedWeapon = 1;
            _m1911UIPanel.SetActive(false);
            _mp5Panel.SetActive(false);
            _f1UIPanel.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            SelectedWeapon = 2;
            _m1911UIPanel.SetActive(false);
            _f1UIPanel.SetActive(false);
            _mp5Panel.SetActive(true);
        }

        if (previousselectedweapon!=SelectedWeapon)
        {
            SelectWeapon();            
        }
    }

    void SelectWeapon()
    {
        int i = 0;

        foreach(Transform weapon in transform)
        {
            if (i == SelectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
