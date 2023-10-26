using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas uiDisplayObject;
    private UIDisplay uiDisplay;
    public bool IsGameStarted { get; private set; } = false;
    public static GameManager Instance { get; private set; }

    private int enemyCount = 0;

    private int powerupCount = 0;

    private bool waitingForPowerUp = false;

    public int Score { get; private set; } = 0;
    public PlayerController playerController;

    [SerializeField] private float beforeNewRoundDelay = 1f;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        uiDisplay = uiDisplayObject.GetComponent<UIDisplay>();
        Time.timeScale = 1;
        
    }
    private void Start()
    {
        
        if (MainManager.Instance.SavedWave != 1)
        {
            MainManager.Instance.CurWave = MainManager.Instance.SavedWave;
            playerController.SendMessage("resetHealthToValue", MainManager.Instance.SavedPlayerHealth);
            Score = MainManager.Instance.SavedPlayerScore;
            uiDisplay.SendMessage("UpdateScore", MainManager.Instance.SavedPlayerScore);
        }
        else
        {
            int zeroScore = 0;
            uiDisplay.SendMessage("UpdateScore", zeroScore);
            // MainManager.Instance.CurWave = 1;
        }
        waitingForPowerUp = true;
        SpawnManager.Instance.InitPowerupGeneration();
       
    }

    public void CheckEnemiesLeft()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0 && MainManager.Instance.isGameActive)
        {
            EndRound();

        }
    }
    public void CheckPowerups()
    {
        powerupCount = GameObject.FindGameObjectsWithTag("Powerup").Length;
        if (powerupCount == 0 && !waitingForPowerUp)
        {
            waitingForPowerUp = true;
            SpawnManager.Instance.InitPowerupGeneration();
        }
    }
    public void SetPowerupWaiting(bool val)
    {
        waitingForPowerUp = val;
    }
    private void StartRound()
    {
        //Debug.Log("S" + Time.time);
        uiDisplay.SendMessage("UpdateWave", MainManager.Instance.CurWave);
        SpawnManager.Instance.SpawnEnemyWave(MainManager.Instance.CurWave);
        SpawnManager.Instance.DestroyObstacles();
        SpawnManager.Instance.GenerateObstacles();
        SpawnManager.Instance.GenerateBackground();
        SpawnManager.Instance.WeatherGeneration();
        uiDisplay.SendMessage("Countdown");
    }
    private void EndRound()
    {
        MainManager.Instance.CurWave++;
        if (MainManager.Instance.CurWave > MainManager.Instance.MaxWave)
        {
            MainManager.Instance.SetMaxWave(MainManager.Instance.CurWave);
        }
        MainManager.Instance.SavePlayerData();
        
        Invoke("StartRound", beforeNewRoundDelay);
    }

    public void GameOver()
    {
        if (Score > MainManager.Instance.GetBestScore())
        {
            uiDisplay.SendMessage("ShowBestScoreMenu");
        }
        else
        {
            uiDisplay.SendMessage("ShowGameOverMenu");
        }
        MainManager.Instance.isGameActive = false;
        MainManager.Instance.CurWave = 1;
        MainManager.Instance.SavePlayerData();

    }
    private void UpdateScore(int addScore)
    {
        Score += addScore;
        uiDisplay.SendMessage("UpdateScore", Score);
    }
    private void ResetScore()
    {
        Score = 0;
    }
       
}
