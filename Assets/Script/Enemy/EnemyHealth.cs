using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float _hitPoints = 100f;
    Animator _animator;
    bool _isdead = false;

    [SerializeField] GameObject _ammo;

    private SpawnZombie _spawnZombie;

    public bool IsDead()
    {
        return _isdead;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spawnZombie = GetComponent<SpawnZombie>();
    }
    public void TakeDamage(float damage)
    {
        _hitPoints -= damage;
        BroadcastMessage("OnDamageTaken");
        Debug.Log("Damaged: "+ damage.ToString());
        if (_hitPoints <= 0)
        {
            if (gameObject.tag == "Titan")
            {
                _spawnZombie.SpawnEnemy();
            }
            Die();
        }
    }

    private void Die()
    {
        if (_isdead) return;
        _isdead = true;
        _animator.SetTrigger("die");
        int randConcreteFactory = Random.Range(0, 1);
        if (randConcreteFactory == 0)
        {
            GameObject.Find("AmmoFactory").GetComponent<ConcreteFactoryBasicAmmo>().increaseIndex();
            GameObject.Find("AmmoFactory").GetComponent<ConcreteFactoryBasicAmmo>().CreateAmmo(transform.position);
        }    
        else
        {
            GameObject.Find("AmmoFactory").GetComponent<ConcreteFactoryBasicAmmo>().increaseIndex();
            GameObject.Find("AmmoFactory").GetComponent<ConcreteFactoryBasicAmmo>().CreateAmmo(transform.position);
        }

    }
}
