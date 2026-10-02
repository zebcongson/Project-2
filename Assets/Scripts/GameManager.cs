using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public GameObject revealObject;
    bool hasRevealed = false;
    #region Unity_functions
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (!hasRevealed)
        {
            GameObject[] remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            Debug.Log("Enemies remaining: " + remainingEnemies.Length);

            if (remainingEnemies.Length == 0)
            {
                if (revealObject != null)
                {
                    revealObject.SetActive(true);
                    hasRevealed = true;
                }
                else
                {
                    Debug.LogWarning("revealObject is not assigned in the Inspector!");
                }
            }
        }
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
        SceneManager.LoadScene("Floor1");
    }

    public void LoseGame()
    {
        /* TODO 7.2: Change the scene when this function is called to the appropriate scene using SceneManager.LoadScene() */
        SceneManager.LoadScene("LoseScene");
    }

    public void WinRound()
    {
        /* TODO 7.2: Change the scene when this function is called to the appropriate scene using SceneManager.LoadScene() */
        if (IsSceneActive("Floor1"))
        {
            SceneManager.LoadScene("Floor2");
        }
        else if (IsSceneActive("Floor2"))
        {
            SceneManager.LoadScene("Floor3");
        }
        else if (IsSceneActive("Floor3"))
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