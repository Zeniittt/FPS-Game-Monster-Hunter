using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecoratorWeapon : AWeapon
{
    private AWeapon _weapon;

    public DecoratorWeapon(AWeapon inner)
    {
        this._weapon = inner;
    }

    public override void Shoot()
    {
        _weapon.Shoot();
    }
}
