using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    #region Unity_functions
    private void Awake() {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    bool IsSceneActive(string sceneName)
    {
        return SceneManager.GetActiveScene().name == sceneName;
    }
    #endregion

        #region Scene_transitions
    public void StartGame()
    {
        /* TODO 7.2: Change the scene when this function is called to the appropriate scene using SceneManager.LoadScene() */
        SceneManager.LoadScene("Level_1");
    }

    public void LoseGame()
    {
        /* TODO 7.2: Change the scene when this function is called to the appropriate scene using SceneManager.LoadScene() */
        SceneManager.LoadScene("LoseScene");
    }

    public void WinRound()
    {
        /* TODO 7.2: Change the scene when this function is called to the appropriate scene using SceneManager.LoadScene() */
        if (IsSceneActive("Level_1"))
        {
            SceneManager.LoadScene("Level_2");
        }
        else if (IsSceneActive("Level_2"))
        {
            SceneManager.LoadScene("Level_3");
        }
        else if (IsSceneActive("Level_3"))
        {
            SceneManager.LoadScene("WinScene");
        }
        
    }

    public void MainMenu()
    {
        /* TODO 7.2: Change the scene when this function is called to the appropriate scene using SceneManager.LoadScene() */
        SceneManager.LoadScene("MainMenu");
    }
    public void Progress_to_2()
    {
        SceneManager.LoadScene("Level_2");
    }
    #endregion
}
