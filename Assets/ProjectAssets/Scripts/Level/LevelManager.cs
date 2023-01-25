using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public event Action OnLevelStart;
    public event Action OnLevelPlay;
    public event Action OnLevelWin;
    public event Action OnLateWin;
    public event Action OnLevelLost;
    public event Action OnLateLost;

    [Space] [Header("LevelLoader")] public LevelLoader LevelLoader;

    [Space] [Header("Time")] [Space] [SerializeField]
    private float _timeWaitLose;

    [SerializeField] private float _timeWaitWin;


    private void Awake()
    {                          
       Time.timeScale = 0;
    }

    public void LevelStart()
    {
        Time.timeScale = 1;

        Taptic.Success();
        OnLevelStart?.Invoke();
    }

    public void LevelPlay()
    {
        OnLevelPlay?.Invoke();
    }
    public void LevelLost()
    {
        Taptic.Failure();
        OnLevelLost?.Invoke();
        Invoke(nameof(LateLost), _timeWaitLose);
    }

    private void LateLost()
    {
        OnLateLost?.Invoke();
        Time.timeScale = 0;
    }

    public void LevelWin()
    {
        Taptic.Success();
        OnLevelWin?.Invoke();
        Invoke(nameof(LateWin), _timeWaitWin);
    }

    private void LateWin()
    {
        OnLateWin?.Invoke();
        Time.timeScale = 0;
    }

                          
    public void LoadNextLevel()
    {
        LevelLoader.LoadNextLevel();
    }

    public void RestartScene()
    {
        LevelLoader.RestartScene();
    }

   
    public void ClearProgress()
    {
        PlayerPrefs.DeleteAll();
    }
}
