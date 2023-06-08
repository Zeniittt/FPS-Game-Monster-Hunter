
using UnityEngine;

public abstract class AmmoType : MonoBehaviour
{
    protected internal string _ammoName;
    protected internal int _ammoAmount;
    protected abstract void OnTriggerEnter(Collider other);
}