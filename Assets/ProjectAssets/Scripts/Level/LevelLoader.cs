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
        if (NumScene == 0) NumScene = 1;
        
        LoadScene();    
    }

    public void LoadNextLevel()
    {
        NumScene += 1;
        
        LoadScene();           
    }

    public void LoadScene()
    {
        int numLoadedScene = NumScene;

        if (numLoadedScene <=ScenesList.Count)
            {
            numLoadedScene -= 1;
            }
        if (numLoadedScene > ScenesList.Count)
            {
            numLoadedScene = 0;
            }
        Debug.Log("Load Scene Number " + numLoadedScene);

        SceneManager.LoadScene(ScenesList[numLoadedScene]);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
