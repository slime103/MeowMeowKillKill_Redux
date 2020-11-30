using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text _text;
    
    private GameManager _gm;
    private LevelManager _lm;

    void Start()
    {
        _text = GetComponent<Text>();
        _gm = GameManager.Instance;
        _lm = LevelManager.Instance;

        StartCoroutine(UpdateText());
    }

    void Update()
    {
    }

    // gets values from game & level managers
    IEnumerator UpdateText()
    {
        while (enabled)
        {
            if (GameManager.Instance.levelStarted)
            {
                var timeLeft = _lm.levels[_lm._levelIndex].timeLimit - _gm.secondTimer;
                _text.text = Mathf.RoundToInt(timeLeft).ToString();
            
                // LOSE GAME
                if (timeLeft <= 0)
                {
                    LevelManager.Instance.LevelWon(false);
                }
            }
            
            yield return new WaitForSeconds(.1f);
        }
    }

    public void EndLevel()
    {
        Destroy(GameManager.Instance.gameObject);
        // Destroy(LevelManager.Instance.gameObject);
        SceneManager.LoadScene("Hub");
    }
}
