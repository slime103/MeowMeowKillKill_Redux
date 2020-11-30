using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static GameManager me;

    public bool levelStarted = false;
    public int timer;
    public float secondTimer;

    public Text text;

    [HideInInspector] public int ratNum = 1;

    void Awake()
    {
        Debug.Log(GameManager.Instance);
        me = this;
    }

    void Update()
    {
        // WIN GAME
        if (levelStarted)
        {
            if (ratNum == 0 && timer > 120)
            {
                LevelManager.Instance.LevelWon(true);
                /*
                text.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene("SampleScene");
                }
            */
            }
            secondTimer += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (levelStarted)
        {
            timer++;
        }
    }
}
