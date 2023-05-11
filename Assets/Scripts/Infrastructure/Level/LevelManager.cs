using System;

public class LevelManager
{
    public event Action OnLevelStart;
    public event Action OnLevelPlay;
    public event Action OnLevelWin;
    public event Action OnLevelLost;
    

    private LevelLoader levelLoader;

    public LevelManager(LevelLoader levelLoader1)
    {
        levelLoader = levelLoader1;
    }
    public void LevelStart()
    {
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
    }

    public void LevelWin()
    {
        Taptic.Success();
        OnLevelWin?.Invoke();
    }

   
    public void LoadNextLevel()
    {
        levelLoader.LoadNextLevel();
    }

    public void RestartScene()
    {
        levelLoader.RestartScene();
    }
}
