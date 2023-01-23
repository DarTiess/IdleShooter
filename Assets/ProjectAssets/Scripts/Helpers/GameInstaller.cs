using DG.Tweening;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]private GameObject _levelManager;
    [SerializeField]private GameObject _canvasController;
    [SerializeField]private GameObject _playerPrefab;

    [Header("Player Position Settings")]
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;

    public override void InstallBindings()
    { 
        Container.Bind<LevelManager>().FromComponentOn(_levelManager).AsSingle();
        Container.Bind<CanvasControl>().FromComponentOn(_canvasController).AsSingle();
        float xPos=Random.Range(_startPosition.position.x,_endPosition.position.x);
        float zPos=Random.Range(_startPosition.position.z,_endPosition.position.z);
        Vector3 playerPosition=new Vector3(xPos, _startPosition.position.y, zPos);
      var _player= Container.InstantiatePrefabForComponent<PlayerMovement>(_playerPrefab, playerPosition,Quaternion.identity, null);
      Container.Bind<PlayerMovement>().FromInstance(_player).AsSingle();
       
    }
}