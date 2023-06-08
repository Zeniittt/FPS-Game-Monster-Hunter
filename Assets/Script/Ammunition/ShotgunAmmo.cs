using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAmmo : AmmoType
{
    private string ammoName = "Shotgun Bullet";
    private int ammoAmount = 3;
    protected override void OnTriggerEnter(Collider other)
    {
        throw new System.NotImplementedException();
    }
}
