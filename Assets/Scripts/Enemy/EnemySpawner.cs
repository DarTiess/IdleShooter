using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class EnemySpawner :  IGetNearestEnemy
{
    public event Action AllEnemiesIsDead;
    private readonly List<string> _keyList;

    private readonly Transform startPosition;
     private readonly Transform endPosition;


   private readonly LevelManager _levelManager;
   private readonly Transform _player;
    private readonly Economics _economics;
    private List<EnemyMovement> _enemyOnScene = new List<EnemyMovement>();
    private List<AsyncOperationHandle> _handlers = new List<AsyncOperationHandle>();

    public EnemySpawner(LevelManager levelManager, 
                        Transform player, 
                        Economics economics, 
                        List<string> enemyKeyList,
                        Transform startTransform, Transform endTransform)
    {
        _levelManager = levelManager;
        _player = player;
        _keyList = new List<string>(enemyKeyList.Count);
        _keyList = enemyKeyList;
        startPosition = startTransform;
        endPosition = endTransform;
        _levelManager.OnLevelPlay += OnPlay;
      
        _economics = economics;
        _economics.OnGetMoney += DeleteEnemyFromList;
        SpawnEnemiesAsync();
    }

     private void Clean()
    {
        foreach (var handler in _handlers)
        {
            Addressables.Release(handler);
        }
        _levelManager.OnLevelPlay -= OnPlay;
    }
    private void DeleteEnemyFromList(EnemyMovement enemy)
    {
        _enemyOnScene.Remove(enemy);
    }

    private void OnPlay()
    {
        foreach (EnemyMovement enemy in _enemyOnScene)
        {
            enemy.OnPlay(_player, _economics);
        }
    }


    private async void SpawnEnemiesAsync()
    {
        for (int i = 0; i < _keyList.Count; i++)
        {

           var loadAssync = Addressables.LoadAssetAsync<GameObject>(_keyList[i]);
            _handlers.Add(loadAssync);
            await loadAssync.Task;
            InitializeEnemyToList(loadAssync.Result);
        }
    }

   
    private void InitializeEnemyToList(GameObject obj)
    {
        float xPos = Random.Range(startPosition.position.x, endPosition.position.x);
        float zPos = Random.Range(startPosition.position.z, endPosition.position.z);
        Vector3 setPosition = new Vector3(xPos, 0, zPos);

        EnemyMovement enemy = Object.Instantiate(obj.GetComponent<EnemyMovement>(), setPosition,Quaternion.identity);
        _enemyOnScene.Add(enemy);
    }
    public Transform GetNearestEnemy(Transform target)
    {
        Transform enemyPosition = null;

        float distanceMin = 100;
        foreach (EnemyMovement enemy in _enemyOnScene)
        {
            float distance = Vector3.Distance(target.position, enemy.gameObject.transform.position);
            if (distance < distanceMin)
            {
                distanceMin = distance;
                enemyPosition = enemy.gameObject.transform;
            }
        }
        if (enemyPosition == null)
        {
            AllEnemiesIsDead?.Invoke();
        }
        return enemyPosition;
    }
}
