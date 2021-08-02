using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int SelectedWeapon = 0;

    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        int previousselectedweapon = SelectedWeapon;

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectedWeapon = 0;
        }

        if(Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >=2)
        {
            SelectedWeapon = 1;
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
