using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int Coin = 0;

    [Header("Game time Display")]
    [SerializeField] Text CoinDisplay;
    [SerializeField] Text LifeDIsplay;
    [SerializeField] Text timerDisplay;
    [SerializeField] GameObject popUpReuslt;
    [Header("Result Display")]
    [SerializeField] Text result_cointDisplay;
    [SerializeField] Text result_timerDisplay;

    private float timer;
    private bool win = false;

    private void Awake()
    {
        popUpReuslt.SetActive(false);
        int numberOfGameSession = FindObjectsOfType<GameSession>().Length;
        if (numberOfGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        CoinDisplay.text = "Coin: " + Coin.ToString();
        LifeDIsplay.text = "Life: " + playerLives.ToString();
    }

    public void playerDie()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            PopUpResult();
        }
    }
    private void Update()
    {
        if (!win)
        {
            timer += Time.deltaTime;
            int min = Mathf.RoundToInt(timer / 60);
            float s = timer % 60;
            if (min < 1)
            {
                timerDisplay.text = ($"Timer: {Math.Round(s, 1)}");
            }
            else
            {
                timerDisplay.text = ($"Timer: {min}m {Math.Round(s, 1)}");
            }
        }
    }

    public void WinTheGame()
    {
        win = true;
        PopUpResult();
    }

    private void TakeLife()
    {
        playerLives--;
        LifeDIsplay.text = "Life: " + playerLives.ToString();
        StartCoroutine(DieDelay());
    }

    IEnumerator DieDelay()
    {
        yield return new WaitForSeconds(2);
        int currentSceneIndedx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndedx);
    }

    public void ResetTheGame()
    {
        SceneManager.LoadScene(1);
        var AllScenePersistance = FindObjectsOfType<ScenePersistance>();

        foreach (var scenePer in AllScenePersistance)
        {
            print(scenePer.gameObject.name);
            Destroy(scenePer.gameObject);
        }
        Destroy(gameObject);
    }

    public void AddToCoint()
    {
        Coin += 1;
        CoinDisplay.text = "Coin: " + Coin.ToString();
    }
    private void PopUpResult()
    {
        CoinDisplay.gameObject.SetActive(false);
        timerDisplay.gameObject.SetActive(false);
        LifeDIsplay.gameObject.SetActive(false);

        popUpReuslt.SetActive(true);
        result_cointDisplay.text = Coin.ToString();
        int min = Mathf.RoundToInt(timer / 60);
        float s = timer % 60;
        if (min < 1)
        {
            result_timerDisplay.text = ($"{Math.Round(s, 1)}s");
        }
        else
        {
            result_timerDisplay.text = ($"{min}m {Math.Round(s, 1)}s");
        }
    }

    public void BackToMenu()
    {
        popUpReuslt.SetActive(false);
        Destroy(FindObjectOfType<MusicPlayer>().gameObject);
        SceneManager.LoadScene("MainMenu");
        var AllScenePersistance = FindObjectsOfType<ScenePersistance>();

        foreach (var scenePer in AllScenePersistance)
        {
            print(scenePer.gameObject.name);
            Destroy(scenePer.gameObject);
        }
        Destroy(gameObject);
    }
}
