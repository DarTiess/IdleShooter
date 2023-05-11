using System.Collections.Generic;
using Scripts.Infrastructure.Input;
using Scripts.SceneObjects;
using Scripts.UI;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


namespace Scripts.Infrastructure
{
    public class EntryPoint: MonoBehaviour
    {
        [FormerlySerializedAs("_startPosition")]
        [Header("Player Position Settings")]
        [SerializeField] private Transform _startPlayerSpawnPosition;
        [FormerlySerializedAs("_endPosition")]
        [SerializeField] private Transform _endPlayerSpawnPosition;
        [SerializeField] private PlayerMovement _playerPrefab;
        [FormerlySerializedAs("_uiPrefab")]
        [FormerlySerializedAs("_canvasPrefab")]
        [Header("UI")]
        [SerializeField] private UIWindowControl _uiWindowPrefab;
        
        [Header("Level Settings")]
        [SerializeField] private LevelLoader _levelLoader;
        [Header("EnemySpawn Settings")]
        [SerializeField] private List<string> _keyList;
        [SerializeField] private Transform _startEnemySpawnPosition;
        [SerializeField] private Transform _endEnemySpawnPosition;
        [SerializeField] private FinishLine _finishLine;
      
        private LevelManager levelManager;
        private IInputService inputService;
        private PlayerMovement player;
        private UIWindowControl _uiWindow;
        private EnemySpawner enemySpawner;
        private Economics economics;

        private void Awake()
        {
            levelManager = new LevelManager(_levelLoader);
            CreateAndInitUI();
           
            inputService = InputService();
            CreatePlayer();
            CreateEnemySpawner();
            InitPlayer();
            HideFinishLine();
        }

        private void CreateAndInitUI()
        {
            _uiWindow = Instantiate(_uiWindowPrefab);
            economics = _uiWindow.GetComponent<Economics>();
            _uiWindow.Init(levelManager);
        }

        private IInputService InputService()
        {
            if (Application.isEditor)
            {
                return new StandaloneInputService();
            }
            else
            {
                return new MobileInputService();
            }
        }

        private void CreatePlayer()
        {
            float xPos = Random.Range(_startPlayerSpawnPosition.position.x, _endPlayerSpawnPosition.position.x);
            float zPos = Random.Range(_startPlayerSpawnPosition.position.z, _endPlayerSpawnPosition.position.z);
            Vector3 playerPosition = new Vector3(xPos, _startPlayerSpawnPosition.position.y, zPos);

            player = Instantiate(_playerPrefab, playerPosition, Quaternion.identity);
           
        }

        private void CreateEnemySpawner()
        {
            enemySpawner = new EnemySpawner(levelManager, player.transform, economics, _keyList, _startEnemySpawnPosition, _endEnemySpawnPosition);
            enemySpawner.AllEnemiesIsDead += ShowFinishLine;
        }

        private void InitPlayer()
        {
            player.Init(inputService, levelManager, enemySpawner);
        }

        private void HideFinishLine()
        {
            _finishLine.Hide();
        }

        private void ShowFinishLine()
        {
            _finishLine.Show();
        }
    }
}