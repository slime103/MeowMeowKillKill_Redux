using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public int _levelIndex = 0;
    public List<Level> levels;

    private void Awake()
    {
        Debug.Log(LevelManager.Instance);
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Hub")
        {
            InitializeHub();
        }
    }

    void Update()
    {
        
    }

    // called by LoadLevel, loads level and updates current level/index
    public void LoadLevel(string l)
    {
        if (l == "Hub")
        {
            SceneManager.LoadScene(l);
            InitializeHub();
            Debug.Log("hub loaded");
        }
        else
        {
            var n = -1;
            for (int i = 0; i < levels.Count; i++)
            {
                if (levels[i].sceneName == l)
                {
                    n = i;
                    Debug.Log("level index: " + i);
                }
            }

            if (n == -1)
            {
                Debug.Log("level not found");
            }
            else
            {
                _levelIndex = n;
            }
            SceneManager.LoadScene(l);
        }
    }

    // disables inactive levels, gets button objects
    // buttons are named by scene
    private void InitializeHub()
    {
        // get buttons
        for (int i = 0; i < levels.Count; i++)
        {
            var n = GameObject.Find("Canvas").transform.Find(levels[i].sceneName).gameObject;
            levels[i].button = n;
        }

        /*
        // disable inactive
        for (int i = 0; i < levels.Count; i++)
        {
            if (i != 0)
            {
                if (!levels[i-1].completed)
                {
                    levels[i].button.SetActive(false);
                }
            }
        }*/
    }

    public void LevelWon(bool b)
    {
        if (b)
        {
            // you win!
            Debug.Log("win");
            
        }
        else
        {
            // you lose :( but it's fine
            Debug.Log("lose");
        }

        if (GameObject.Find("CatUI"))
        {
            GameObject.Find("CatUI").transform.Find("Timer").GetComponent<Timer>().EndLevel();
        }
        else
        {
            Debug.Log("timer not found - can't self destruct");
        }
    }
}
