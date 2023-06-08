using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteFactoryBasicAmmo : AmmoFactory
{
    [SerializeField] GameObject[] _ammo;
    static int indexAmmo = 0;
    public override GameObject CreateAmmo(Vector3 position)
    {
        GameObject ammoModel = null;
        if (ConcreteFactoryBasicAmmo.indexAmmo == 0)
        {
            ammoModel = _ammo[0];
        }
        if (indexAmmo == 1)
        {
            ammoModel = _ammo[1];
        }
        if (indexAmmo == 2)
        {
            ammoModel = _ammo[2];
        }
        if (ammoModel != null)
        {
            Debug.Log(indexAmmo);
            GameObject ammoObject = Instantiate(ammoModel, position, Quaternion.identity);
            return ammoObject;
        }
        return null;
    }

    public void increaseIndex()
    {
        if (ConcreteFactoryBasicAmmo.indexAmmo < 2)
        {
            ConcreteFactoryBasicAmmo.indexAmmo++;
        }
        else
        {
            ConcreteFactoryBasicAmmo.indexAmmo = 0;
        }
    }

}
