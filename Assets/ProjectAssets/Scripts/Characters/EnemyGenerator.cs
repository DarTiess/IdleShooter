using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private List<string> _keyList;    
    [Header("Position Settings")]
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    [SerializeField] private GameObject _finishLine;

    LevelManager _levelManager;
    GameObject _player;
    Economics _economics;
    List<EnemyMovement> _enemyOnScene = new List<EnemyMovement>();

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
            await loadAssync.Task;
            InitializeEnemyToList(loadAssync.Result);
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
