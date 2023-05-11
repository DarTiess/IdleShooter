using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class UIWindowControl : MonoBehaviour
    {

        [Header("Panels")]
        [SerializeField] private WindowBase _panelMenu;
        [SerializeField] private WindowBase _panelInGame;
        [SerializeField] private WindowBase _panelWin;
        [SerializeField] private WindowBase _panelLost;
        [SerializeField] private CanvasGroup _panelHeader;
        [SerializeField] private Text _timer;
        [SerializeField] private float _timeToStart;
 
        private LevelManager _levelManager;
        public void Init(LevelManager LevelManager)
        {
            _levelManager = LevelManager;
    
            _levelManager.OnLevelStart += OnLevelStart;
            _levelManager.OnLevelWin += OnLevelWin;
            _levelManager.OnLevelLost += OnLevelLost;

            _panelMenu.ClickedPanel += LevelStart;
            _panelWin.ClickedPanel += LoadNextLevel;
            _panelLost.ClickedPanel += RestartLevel;

            _panelHeader.alpha = 1;
            _panelHeader.interactable = true;
            _panelHeader.blocksRaycasts = true;
            HideAllPanels();
            _panelMenu.Show();
        }

        private void OnDisable()
        {
            _levelManager.OnLevelStart -= OnLevelStart;
            _levelManager.OnLevelWin -= OnLevelWin;
            _levelManager.OnLevelLost -= OnLevelLost;
        }

        private void OnLevelStart()
        {
            HideAllPanels();
            _panelInGame.Show();
            int starting = (int)_timeToStart;
            _timer.DOCounter(starting, 0, _timeToStart)
                  .OnComplete(() =>
                  {
                      _levelManager.LevelPlay();
                      _timer.gameObject.SetActive(false);
                  });
        }

        private void HideAllPanels()
        {
            _panelInGame.Hide();
            _panelMenu.Hide();
            _panelLost.Hide();
            _panelWin.Hide();
        }

        private void OnLevelWin()
        {
            Debug.Log("Level Win");
            HideAllPanels();
            _panelWin.Show();
        }

        private void OnLevelLost()
        {
            Debug.Log("Level Lost");
            HideAllPanels();
            _panelLost.Show();
        }
    
        private void LoadNextLevel()
        {
            _levelManager.LoadNextLevel();
        }
        private void LevelStart()
        {
            _levelManager.LevelStart();
        }

        private void RestartLevel()
        {
            _levelManager.RestartScene();
        }
    }
}
