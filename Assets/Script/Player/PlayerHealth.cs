using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _health = 100;
    DeathHandler _deathHandler;
    private void Start()
    {
        _deathHandler = GetComponent<DeathHandler>();

        if(_deathHandler == null)
        {
            Debug.LogError("Death Handler Is Null");
        }
    }
    void Update()
    {
        if(_health <= 0)
        {
            _deathHandler.HandleDeath();
        }
    }

    public void Damage(float damage)
    {
        _health -= damage;
    }
}
