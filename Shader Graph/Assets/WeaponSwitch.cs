using UnityEngine;
using TMPro;

public class WeaponSwitch : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _m1911UIPanel;
    [SerializeField] private GameObject _f1UIPanel;
    [SerializeField] private TextMeshProUGUI _currBulletsText;
    [SerializeField] private TextMeshProUGUI _maxBulletsText;

    public Weapon _weapon;
    public static int SelectedWeapon = 0;

    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        if (SelectedWeapon == 0)
        {
           _currBulletsText.text = _weapon.BulletInMag.ToString();
           _maxBulletsText.text = _weapon.MaxBulletsInMag.ToString();
    
        }


        int previousselectedweapon = SelectedWeapon;

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectedWeapon = 0;
            _f1UIPanel.SetActive(false);
            _m1911UIPanel.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >=2)
        {
            SelectedWeapon = 1;
            _m1911UIPanel.SetActive(true);
            _f1UIPanel.SetActive(true);
        }

        if(previousselectedweapon!=SelectedWeapon)
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
