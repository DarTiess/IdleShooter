using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerAttack))]
public class PlayerMovement : MonoBehaviour
{

    [Header("Settings")] 
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _rotationSpeed;

    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    // components
    [Space] [Header("Components && Scripts")]
   
    private NavMeshAgent _nav;

    private PlayerAnimator _anim;
    private PlayerAttack _playerAttack;
    [HideInInspector]public EnemyGenerator _enemyGenerator;
    //helpers
    private Vector3 _temp;
    public bool _canMove;

    private LevelManager _levelManager;

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
        _anim = GetComponent<PlayerAnimator>();
        _playerAttack= GetComponent<PlayerAttack>();
        _playerAttack.InitBullets();
    }

    private void FixedUpdate()
    {
       
        if (!_canMove)
        {
            return;
        }
        
        Move();
    }
    Transform enemyPos;
    private void Move()
    {
        float inputHorizontal = SimpleInput.GetAxis(HorizontalAxis);
        float inputVertical = SimpleInput.GetAxis(VerticalAxis);

        if(inputHorizontal==0 && inputVertical == 0)
        {
           enemyPos =_enemyGenerator.GetNearestEnemy();
            if(enemyPos == null)
            {
                return;
            }
            Vector3 lookDirection = enemyPos.position - transform.position;
            if (lookDirection != Vector3.zero)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation,
                    Quaternion.LookRotation(lookDirection), _rotationSpeed * Time.fixedDeltaTime);
            }
            _playerAttack.AttackEnemy(enemyPos);
        }
        else
        {
              _temp.x = inputHorizontal;
            _temp.z = inputVertical;
           
           _anim.MoveAnimation(_temp.magnitude);         
            
            _nav.Move(_temp * _playerSpeed * Time.fixedDeltaTime);

            Vector3 tempDirect = transform.position + Vector3.Normalize(_temp);
            Vector3 lookDirection = tempDirect - transform.position;
            if (lookDirection != Vector3.zero)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation,
                    Quaternion.LookRotation(lookDirection), _rotationSpeed * Time.deltaTime);
            }
        }
          

            
    }
   
}
                 