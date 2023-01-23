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
    [SerializeField]private float _distanceFromPlayer;
   [SerializeField] private float _timeToStay;
    [SerializeField]private int _health;
    [SerializeField]private float _speedAttack;
    [SerializeField] private int _makeDamage;

    private NavMeshAgent _navMesh;
    public bool _canMove;
    private GameObject _player;
 


    public void OnPlay(GameObject player)
    {
        _player=player;
        _canMove=true;
    }


    // Start is called before the first frame update
    void Start()
    {
        _navMesh= GetComponent<NavMeshAgent>();
        _navMesh.speed= _speedMove;
        _navMesh.stoppingDistance=_distanceFromPlayer;
        //поворот в сторону игрока
        // начинают стрельбу
        // если игрок дальше отошел, стоп аттак, подходим стреляем
    }

    // Update is called once per frame
    void Update()
    {
        if(_canMove)
        {
            _navMesh.SetDestination(_player.transform.position);
        }   
    }
}
