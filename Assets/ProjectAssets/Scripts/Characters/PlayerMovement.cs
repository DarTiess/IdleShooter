﻿using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PersonAnimator))]
[RequireComponent(typeof(PersoneAttack))]
[RequireComponent(typeof(HealthBar))]
[RequireComponent(typeof(PlayerInventory))]
[RequireComponent(typeof(PlayerTreeStack))]
public class PlayerMovement : MonoBehaviour, IHealth
{

    [Header("Settings")] 
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _rotationSpeed;

    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    // components
    [Space] [Header("Components && Scripts")]
   
    private NavMeshAgent _nav;

    private PersonAnimator _anim;
    private PersoneAttack _playerAttack;
    private HealthBar _healthBar;
    private PlayerInventory _playerInventory;
    private PlayerTreeStack _playerTreeStack;
    [HideInInspector]public EnemyGenerator _enemyGenerator;
    //helpers
    private Vector3 _temp;
    private bool _canMove;
    private bool _isOnAttack;

    private LevelManager _levelManager;

    [SerializeField]private int _health;
    [Inject]
    private void Initiallization(LevelManager levelManager)
    {
        _levelManager = levelManager;
        _levelManager.OnLevelPlay += OnPlay;
    }

    public void OnPlay()
    {
       _canMove= true;
       _nav=GetComponent<NavMeshAgent>(); 
       
    }

    private void Start()
    {
        _anim = GetComponent<PersonAnimator>();
        _playerAttack= GetComponent<PersoneAttack>();
        _healthBar = GetComponent<HealthBar>();
        _playerInventory= GetComponent<PlayerInventory>();
        _playerTreeStack= GetComponent<PlayerTreeStack>();
        _healthBar.SetMaxValus(_health);
        _playerAttack.InitBullets();
    }

    private void FixedUpdate()
    {
       
        if (!_canMove)
        {
            return;
        }
        
        Action();
    }
    Transform enemyPos;
    private void Action()
    {
        float inputHorizontal = SimpleInput.GetAxis(HorizontalAxis);
        float inputVertical = SimpleInput.GetAxis(VerticalAxis);

        if(inputHorizontal==0 && inputVertical == 0)
        {
           Attack();
        }
        else
        {
           Move(inputHorizontal, inputVertical);
        }            
    }

    void Attack()
    {
        _isOnAttack=true;
       enemyPos =_enemyGenerator.GetNearestEnemy();
        if(enemyPos == null)
        {
            _isOnAttack=false;
          return;
        }
          MakeRotation(enemyPos.position);
          _playerAttack.AttackEnemy(enemyPos);
    }
    void Move(float inputHorizontal, float inputVertical)
    {
        //_isOnAttack=false;
         _temp.x = inputHorizontal;
         _temp.z = inputVertical;
           
         _anim.MoveAnimation(_temp.magnitude); 
         _nav.Move(_temp * _playerSpeed * Time.fixedDeltaTime);

          Vector3 tempDirect = transform.position + Vector3.Normalize(_temp);
          MakeRotation(tempDirect);
    }
    void MakeRotation(Vector3 target)
    {
          Vector3 lookDirection = target- transform.position;
            if (lookDirection != Vector3.zero)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation,
                    Quaternion.LookRotation(lookDirection), _rotationSpeed * Time.fixedDeltaTime);
            }
    }
  

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Tree"))
        {
            if (_isOnAttack)
             {
                StopGettingTree();
             }
             else
             {
               SartingGettingTree(other.transform);
             }    
               
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(!_isOnAttack)
        {
             if (other.gameObject.CompareTag("Tree"))
             {
               StopGettingTree();
             }
        }
       
    }

    void SartingGettingTree(Transform tree)
    {
        _playerInventory.GetMelee();
        _anim.GetTreeAnimation();
      
        _playerTreeStack.TakeTreeBlockToStack(tree);
    }
    public void GetTreeBlock(Transform tree)
    {
      // _playerTreeStack.TakeTreeBlockToStack(tree);
    }
    public void StopGettingTree()
    {
       _playerInventory.GetGun();
       _anim.EndTreeAnimation();
    }
    public void TakeDamage(int attackpower)
    {
        if (_health > 0)
        {
             _health-=attackpower;
        _healthBar.SetBadValues(attackpower);
        }
        else
        {
            _levelManager.LevelLost();
           _anim.DeadAnimation();
            _canMove= false;
        }
    }
}
                 