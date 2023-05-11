using Scripts.UI;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private LevelManager _levelManager;
    [FormerlySerializedAs("_uiController")]
    [FormerlySerializedAs("_canvasController")]
    [SerializeField] private UIWindowControl _uiWindowController;
    [SerializeField] private GameObject _playerPrefab;
    [FormerlySerializedAs("_getNearestEnemy")]
    [FormerlySerializedAs("_enemyGenerator")]
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Economics _economics;

    [Header("Player Position Settings")]
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;

    public override void InstallBindings()
    {
        BindLevelManager();
        BindCanvasController();
        BindEconomics();
        BindPlayer();
        BindEnemiesGenerator();

    }

    private void BindLevelManager()
    {
        Container.Bind<LevelManager>().FromInstance(_levelManager).AsSingle();
    }
    private void BindCanvasController()
    {
        Container.Bind<UIWindowControl>().FromInstance(_uiWindowController).AsSingle();
    }

    void BindPlayer()
    {
       // float xPos = Random.Range(_startPosition.position.x, _endPosition.position.x);
      //  float zPos = Random.Range(_startPosition.position.z, _endPosition.position.z);
     //   Vector3 playerPosition = new Vector3(xPos, _startPosition.position.y, zPos);

      //  var _player = Container.InstantiatePrefabForComponent<PlayerMovement>(_playerPrefab, playerPosition, Quaternion.identity, null);
      //  Container.Bind<PlayerMovement>().FromInstance(_player).AsSingle();
    }
    private void BindEnemiesGenerator()
    {
        Container.Bind<EnemySpawner>().FromInstance(_enemySpawner).AsSingle();
    }

    private void BindEconomics()
    {
        Container.Bind<Economics>().FromInstance(_economics).AsSingle();
    }
}