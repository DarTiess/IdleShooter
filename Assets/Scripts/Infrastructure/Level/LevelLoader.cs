using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelLoader", menuName = "LevelLoader", order = 51)]
public class LevelLoader : ScriptableObject
{
    public List<string> ScenesList;
    public int NumScene
    {
        get { return PlayerPrefs.GetInt("NumOfScene"); }
        set { PlayerPrefs.SetInt("NumOfScene", value); }
    }

    public void StartGame()
    {
        LoadScene();
    }

    public void LoadNextLevel()
    {
        NumScene += 1;

        LoadScene();
    }

    public void LoadScene()
    {
        if (NumScene >= ScenesList.Count)
        {
            NumScene = 0;
        }
        Debug.Log("Load Scene Number " + NumScene);

        SceneManager.LoadScene(ScenesList[NumScene]);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
