using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Transform _target;

    NavMeshAgent _navMeshAgent;

    [SerializeField] float _chaseRange = 5f, _distanceToTarget = Mathf.Infinity;

    [SerializeField] bool _isProvoked = false;

    private Animator _animator;

    [SerializeField] bool _isAttack = false;

    private EnemyHealth _enemyHealth;
    private Rigidbody _rigidbody;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _enemyHealth = GetComponent<EnemyHealth>();
        //_rigidbody = GetComponent<Rigidbody>();
        _target = GameObject.Find("Player").GetComponent<Transform>();
        if (_animator == null)
        {
            Debug.LogError("Animator is Null");
        }

        if (_navMeshAgent == null)
        {
            Debug.LogError("navMeshAgent is Null");
        }

        if (_enemyHealth == null)
        {
            Debug.LogError("enemyHealth is Null");
        }
    }


    void Update()
    {
        if (_enemyHealth.IsDead())
        {
            enabled = false;
            _navMeshAgent.enabled = false;
            Destroy(_rigidbody);
            Destroy(gameObject, 4f);
            return;
        }

        _distanceToTarget = Vector3.Distance(_target.position, transform.position);
        if (_isProvoked)
        {
            EngageTarget();
        }
        else if (_distanceToTarget <= _chaseRange)
        {
            _isProvoked = true;
        }
    }

    public void OnDamageTaken()
    {
        _isProvoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();
        if (_distanceToTarget >= _navMeshAgent.stoppingDistance && _isAttack == false)
        {
            ChaseTarget();
        }

        if (_distanceToTarget <= _navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        _isAttack = true;
        _animator.SetBool("attack", true);
    }

    private void onAttackFinish()
    {
        _isAttack = false;
    }

    private void ChaseTarget()
    {
        _animator.SetBool("attack", false);
        _animator.SetTrigger("move");
        _navMeshAgent.SetDestination(_target.position);
    }

    private void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _chaseRange);
        float test = _navMeshAgent.stoppingDistance + 1.5f;
        Gizmos.DrawWireSphere(transform.position, test);
    }
}
