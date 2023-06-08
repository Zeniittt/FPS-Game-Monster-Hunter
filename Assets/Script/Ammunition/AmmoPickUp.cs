using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    [SerializeField] int _ammoAmount = 10;
    [SerializeField] AmmoType _ammoType;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(_ammoType, _ammoAmount);

            Debug.Log("jfkjhgkdgfjgf");
            Destroy(gameObject);
        }
    }
}
