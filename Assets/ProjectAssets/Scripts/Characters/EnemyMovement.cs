using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Zenject.SpaceFighter;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PersonAnimator))]
[RequireComponent(typeof(PersoneAttack))]
[RequireComponent(typeof(HealthBar))]
public class EnemyMovement : MonoBehaviour, IHealth
{
   [SerializeField] private EnemyBehavior _behavior;
    [SerializeField]private EnemyMove _typeOfMove;
    [SerializeField]private float _speedMove;
    [SerializeField]private float _rotationSpeed;
    [SerializeField]private float _distanceFromPlayer;
   [SerializeField] private float _timeToStay;
    [SerializeField]private int _health;   
    [SerializeField] private int _price;

    private NavMeshAgent _navMesh;
    private PersonAnimator _animator;
    private PersoneAttack _personeAtteck;
     private HealthBar _healthBar;
    private bool _canMove;
    private bool _isDead;
    private GameObject _player;
    private Economics _economics;
    private float _time;

    // Start is called before the first frame update
    void Start()
    {
        _navMesh= GetComponent<NavMeshAgent>();
        _navMesh.speed= _speedMove;
        _navMesh.stoppingDistance=_distanceFromPlayer;
       _animator= GetComponent<PersonAnimator>();
        _personeAtteck= GetComponent<PersoneAttack>();
          _healthBar = GetComponent<HealthBar>();
        _healthBar.SetMaxValus(_health);      
    }
     public void OnPlay(GameObject player, Economics economics)
    {
        _player=player;
        _economics=economics;
        _canMove=true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(_canMove && !_isDead)
        {
           Action();
        }   
    }

    private void Action()
    {
          if(_behavior == EnemyBehavior.Moved)
        {
             if(Vector3.Distance( transform.position,  _player.transform.position) <= _distanceFromPlayer)
        {
           Attack();
        }
        else
        {
            if (_time > 0)
            {
                _time-= Time.deltaTime;
                return;
            }
           Move();
        }
        }
        else
        {
             _navMesh.speed=0;
             Attack();
        }
       
         _animator.MoveAnimation(_navMesh.velocity.magnitude / _navMesh.speed);
        MakeRotation();
    }

    void Attack()
    {
         _personeAtteck.AttackEnemy(_player.transform);
        _time=_timeToStay;
    }
    void Move()
    {
       _navMesh.SetDestination(_player.transform.position);        
    }

    void MakeRotation()
    {
          Vector3 lookDirection = _player.transform.position - transform.position;
            if (lookDirection != Vector3.zero)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation,
                    Quaternion.LookRotation(lookDirection), _rotationSpeed * Time.fixedDeltaTime);
            }
    }
      public void TakeDamage(int attackPower)
    {
        if (_health > 0)
        {
            _healthBar.SetBadValues(attackPower);
       _health-=attackPower;
        }
          else
        {
            OnDestroidEnemy();
        }
    }

    void OnDestroidEnemy()
    {
        _navMesh.isStopped= true;
        _isDead=true;
        gameObject.tag="Untagged";
        _animator.DeadAnimation();
        _economics.GetMoney(_price, this);
    }

}
