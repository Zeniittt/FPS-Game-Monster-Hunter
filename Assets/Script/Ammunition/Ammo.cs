using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    //[SerializeField] int ammoAmount = 20;

    public int GetCurrentAmmo<Unknown>(Unknown ammoType) where Unknown : AmmoType
    {
        return ammoType._ammoAmount;
    }

    public void ReduceCurrentAmmo<Unknown>(Unknown ammoType) where Unknown: AmmoType
    {
        ammoType._ammoAmount--;
    }

    public void IncreaseCurrentAmmo<Unknown>(Unknown ammoType, int ammoAmmount) where Unknown : AmmoType
    {
        ammoType._ammoAmount += ammoAmmount;
    }

    /*private AmmoType GetAmmoType<Unknown>(Unknown ammoType) where Unknown : AmmoType
    {
        if (ammoType != null)
        {
            return ammoType;
        } else return null;
    }*/
}
