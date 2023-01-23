using DG.Tweening;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]private LevelManager _levelManager;
    [SerializeField]private CanvasControl _canvasController;
    [SerializeField]private GameObject _playerPrefab;
    [SerializeField]private EnemyGenerator _enemyGenerator;
    [SerializeField]private Economics _economics;

    [Header("Player Position Settings")]
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;

    public override void InstallBindings()
    { 
       BindLevelManager();
       BindCanvasController();
       BindPlayer();
        BindEnemiesGenerator();
        BindEconomics();
    }

    private void BindLevelManager()
    {
         Container.Bind<LevelManager>().FromInstance(_levelManager).AsSingle();
    }
    private void BindCanvasController()
    {
       Container.Bind<CanvasControl>().FromInstance(_canvasController).AsSingle();
    }

    void BindPlayer()
    {
         float xPos=Random.Range(_startPosition.position.x,_endPosition.position.x);
        float zPos=Random.Range(_startPosition.position.z,_endPosition.position.z);
        Vector3 playerPosition=new Vector3(xPos, _startPosition.position.y, zPos);
      
        var _player= Container.InstantiatePrefabForComponent<PlayerMovement>(_playerPrefab, playerPosition,Quaternion.identity, null);
        Container.Bind<PlayerMovement>().FromInstance(_player).AsSingle();
    }
      private void BindEnemiesGenerator()
    {
       Container.Bind<EnemyGenerator>().FromInstance(_enemyGenerator).AsSingle();
    }
    
    private void BindEconomics()
    {
       Container.Bind<Economics>().FromInstance(_economics).AsSingle();
    }
}