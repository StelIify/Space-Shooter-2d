using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoading : MonoBehaviour
{
    float waitTime = 2f;
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Level one");
        if(FindObjectsOfType<GameSession>().Length == 0)
        {
            return;
        }
        FindObjectOfType<GameSession>().ResetGame();
    }

     public void GameOver()
     {
        StartCoroutine(WaitAndLoad());
     }

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Game Over");
        FindObjectOfType<HealthDispley>().ResetHealth();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

  }