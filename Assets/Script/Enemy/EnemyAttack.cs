using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Transform _target;

    [SerializeField]
    float _damage = 40f;
    PlayerHealth _playerHealth;
    NavMeshAgent _navMeshAgent;
    float _distanceToTarget = Mathf.Infinity;

    void Start()
    {
        _playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (_playerHealth == null)
        {
            Debug.LogError("PlayerHealth is Null");
        }
    }

    private void Update()
    {
        _distanceToTarget = Vector3.Distance(_target.position, transform.position);
    }

    //sự kiện tại 1 thời điểm khi attack animation được trigger
    private void AttackHitEvent()
    {
        if (_playerHealth != null) {
            if(transform.tag == "Zombie" && _distanceToTarget <= _navMeshAgent.stoppingDistance)
            {
                Debug.Log("Gotchu! By Zombie");
                _playerHealth.Damage(_damage);
                _playerHealth.GetComponent<DisplayDamage>().ShowDamageImpact();
            }
            if(transform.tag == "Titan" && _distanceToTarget <= _navMeshAgent.stoppingDistance + 1.5)
            {
                Debug.Log("Gotchu! By Titan");
                _playerHealth.Damage(_damage);
                _playerHealth.GetComponent<DisplayDamage>().ShowDamageImpact();
            }
        }

    }
}
