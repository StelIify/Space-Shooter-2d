using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameSession : MonoBehaviour
{
   
    int currentScore = 0;
    

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSession = FindObjectsOfType<GameSession>().Length;
        if(numberGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddToScore(int scoreValue)
    {
        currentScore += scoreValue;
        
    }
    public int GetScore()
    {
        return currentScore;
    }
    public void ResetGame()
    {
        Destroy(gameObject);
    }



}
