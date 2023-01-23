using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Zenject.SpaceFighter;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
   [SerializeField] private EnemyBehavior _behavior;
    [SerializeField]private EnemyMove _typeOfMove;
    [SerializeField]private float _speedMove;
    [SerializeField]private float _rotationSpeed;
    [SerializeField]private float _distanceFromPlayer;
   [SerializeField] private float _timeToStay;
    [SerializeField]private int _health;
    [SerializeField]private float _speedAttack;
    [SerializeField] private int _makeDamage;
    [SerializeField] private int _price;

    private NavMeshAgent _navMesh;
    private bool _canMove;
    private GameObject _player;
    private Economics _economics;


    public void OnPlay(GameObject player, Economics economics)
    {
        _player=player;
        _economics=economics;
        _canMove=true;
    }

    public void TakeDamage(int attackPower)
    {
       _health-=attackPower;
        if(_health<=0)
        {
            OnDestroidEnemy();
        }
    }

    void OnDestroidEnemy()
    {
        _navMesh.isStopped= true;
          gameObject.SetActive(false);
        _economics.GetMoney(_price, this);
       //добавл€ем мани плееру
       //удал€ем с листа енеми
    }

    // Start is called before the first frame update
    void Start()
    {
        _navMesh= GetComponent<NavMeshAgent>();
        _navMesh.speed= _speedMove;
        _navMesh.stoppingDistance=_distanceFromPlayer;
        //поворот в сторону игрока
        // начинают стрельбу
        // если игрок дальше отошел, стоп аттак, подходим стрел€ем
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_canMove)
        {
           Move();
        }   
    }

    private void Move()
    {
          Vector3 lookDirection = _player.transform.position - transform.position;
            if (lookDirection != Vector3.zero)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation,
                    Quaternion.LookRotation(lookDirection), _rotationSpeed * Time.fixedDeltaTime);
            }
         _navMesh.SetDestination(_player.transform.position);
    }
}
