using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SniperAmmo : AmmoType
{
    private void Start()
    {
        this._ammoAmount = 10; 
    }

    protected override void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(this, 5);

            Debug.Log("jfkjhgkdgfjgf");
            Destroy(gameObject);
        }
    }

    
}
