using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] List<EnemyMovement> _enemyList;
    List<EnemyMovement> _enemyOnScene= new List<EnemyMovement>();
    [Header("Position Settings")]
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;

    LevelManager _levelManager;
    GameObject _player;

    [Inject]
    private void Initialization(LevelManager levelManager, PlayerMovement player)
    {
        _levelManager = levelManager;
        _player=player.gameObject;
        _levelManager.OnLevelPlay += OnPlay;
    }

    private void OnPlay()
    {
        foreach (EnemyMovement enemy in _enemyOnScene)
        {
            enemy.OnPlay(_player);
        }
    }

    private void Start()
    {
         
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < _enemyList.Count; i++)
        {
            float xPos= Random.Range(startPosition.position.x, endPosition.position.x);
            float zPos= Random.Range(startPosition.position.z, endPosition.position.z);
            Vector3 setPosition= new Vector3(xPos, transform.position.y, zPos);
          EnemyMovement enemy=Instantiate(_enemyList[i], setPosition, transform.rotation);
          _enemyOnScene.Add(enemy);
        }
    }
}
