using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int _currentWeapon = 0;


    void Start()
    {
        SetWeaponActive();
    }

    void Update()
    {
        int previousWeapon = _currentWeapon;

        //Get Input
        ProcessKeyInput();
        ProcessScrollWhell();

        //Set Weapon Active 
        if (previousWeapon != _currentWeapon)
        {
            SetWeaponActive();
        }
    }

    private void ProcessScrollWhell()
    {
        if (Input.GetAxis("Mouse ScrollWheel")<0)
        {
            if(_currentWeapon >= transform.childCount - 1)
            {
                _currentWeapon = 0;
            }
            else
            {
                _currentWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (_currentWeapon  <= 0  )
            {
                _currentWeapon = transform.childCount - 1;
            }
            else
            {
                _currentWeapon--;
            }
        }
    }

    private void ProcessKeyInput()
    {
        switch (Input.inputString)
        {
            case "1":
                _currentWeapon = 0;
                break;
            case "2":
                _currentWeapon = 1;
                break;
            case "3":
                _currentWeapon = 2;
                break;
            default:
                break;
        }
    }

    private void SetWeaponActive()
    {
        int weaponIndex = 0;

        foreach (Transform weapon in transform)
        {
            if (weaponIndex == _currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }


}
