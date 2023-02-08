using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private List<string> _keyList;
    [Header("Position Settings")]
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    [SerializeField] private GameObject _finishLine;

    private LevelManager _levelManager;
    private GameObject _player;
    private Economics _economics;
    private List<EnemyMovement> _enemyOnScene = new List<EnemyMovement>();
    private List<AsyncOperationHandle> _handlers = new List<AsyncOperationHandle>();
    [Inject]
    private void Initialization(LevelManager levelManager, PlayerMovement player, Economics economics)
    {
        _levelManager = levelManager;
        _player = player.gameObject;
        _levelManager.OnLevelPlay += OnPlay;
        player._enemyGenerator = this;
        _economics = economics;
        _economics.OnGetMoney += DeleteEnemyFromList;
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

    private void Start()
    {
        _finishLine.SetActive(false);
        SpawnEnemiesAsync();
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

    private void OnDisable()
    {
        foreach (var handler in _handlers)
        {
            Addressables.Release(handler);
        }
    }
    private void InitializeEnemyToList(GameObject obj)
    {
        float xPos = Random.Range(startPosition.position.x, endPosition.position.x);
        float zPos = Random.Range(startPosition.position.z, endPosition.position.z);
        Vector3 setPosition = new Vector3(xPos, transform.position.y, zPos);

        EnemyMovement enemy = Instantiate(obj.GetComponent<EnemyMovement>(), setPosition, transform.rotation);
        _enemyOnScene.Add(enemy);
    }
    public Transform GetNearestEnemy()
    {
        Transform enemyPosition = null;

        float distanceMin = 100;
        foreach (EnemyMovement enemy in _enemyOnScene)
        {
            float distance = Vector3.Distance(_player.transform.position, enemy.gameObject.transform.position);
            if (distance < distanceMin)
            {
                distanceMin = distance;
                enemyPosition = enemy.gameObject.transform;
            }
        }
        if (enemyPosition == null)
        {
            _finishLine.SetActive(true);
        }
        return enemyPosition;
    }
}
