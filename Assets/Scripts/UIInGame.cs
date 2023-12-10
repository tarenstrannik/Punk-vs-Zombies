using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInGame : UIDisplay
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject gameoverScreen;
    [SerializeField] private GameObject bestscoreScreen;
    [SerializeField] private GameObject inGameUIScreen;

    [SerializeField] private GameObject waveNumberCountdownPanel;
    [SerializeField] private TextMeshProUGUI waveNumberCountdownText;
    [SerializeField] private float countdownTimer = 1f;
    private bool isCountdown=false;

    [SerializeField] private PlayerCustomInput m_playerInput;

    private Coroutine countdownCoroutine;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }
    private void UpdateHealth(int health)
    {
        healthText.text = $"Health: {health}";
    }
    private void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }
    private void UpdateWave(int wave)
    {
        waveText.text = $"Wave: {wave}";
    }

    public override void OpenMenu(UnityEngine.Object obj)
    {
             
        GameObject target = obj as GameObject;
        if (target == null && MainManager.Instance.isGameActive && (curMenu==null || curMenu== pauseScreen) &&!isCountdown)
        {
            
            Time.timeScale = Time.timeScale == 1f ? 0f : 1f;
            pauseScreen.SetActive(!pauseScreen.activeSelf);
            inGameUIScreen.SetActive(!inGameUIScreen.activeSelf);
            curMenu = pauseScreen.activeSelf==true ? pauseScreen : null;
            m_playerInput.ResetSticks();
        }
        else if (target == null && MainManager.Instance.isGameActive && (curMenu == null || curMenu == pauseScreen) && isCountdown)
        {
            StopCoroutine(countdownCoroutine);

            waveNumberCountdownPanel.SetActive(false);
            Time.timeScale = 1f;
            isCountdown = false;
            inGameUIScreen.SetActive(true);
            
        }

            base.OpenMenu(obj);
    }

    private void ShowGameOverMenu()
    {
        inGameUIScreen.SetActive(!inGameUIScreen.activeSelf);
        gameoverScreen.SetActive(true);
        curMenu = gameoverScreen;
    }

    private void ShowBestScoreMenu()
    {
        inGameUIScreen.SetActive(!inGameUIScreen.activeSelf);
        bestscoreScreen.SetActive(true);
        curMenu = bestscoreScreen;
        prevMenu = gameoverScreen;
    }
    public void SetPlayerResult()
    {
        MainManager.Instance.SetCurPlayerScore();
        MainManager.Instance.SavePlayerData();
    }
    private void Countdown()
    {

        countdownCoroutine= StartCoroutine(WaveStartBackCount());
    }
    IEnumerator WaveStartBackCount()
    {
        isCountdown = true;
        Time.timeScale = 0f;

        waveNumberCountdownPanel.SetActive(true);
        inGameUIScreen.SetActive(false);
        waveNumberCountdownText.text = "Wave " + MainManager.Instance.CurWave;
        yield return new WaitForSecondsRealtime(countdownTimer);
        waveNumberCountdownText.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        waveNumberCountdownText.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        waveNumberCountdownText.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        waveNumberCountdownText.text = "Start!";
        yield return new WaitForSecondsRealtime(countdownTimer);
        waveNumberCountdownPanel.SetActive(false);
        Time.timeScale = 1f;
        isCountdown = false;
        m_playerInput.ResetSticks();
        inGameUIScreen.SetActive(true);

    }

}
