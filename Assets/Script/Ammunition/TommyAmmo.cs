using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TommyAmmo : AmmoType
{
    private string ammoName = "Tommy Bullet";
    private int ammoAmount = 10;
    protected override void OnTriggerEnter(Collider other)
    {
        throw new System.NotImplementedException();
    }
}
