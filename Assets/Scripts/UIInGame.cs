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
        if (target == null && MainManager.Instance.isGameActive && (curMenu==null || curMenu== pauseScreen))
        {
            
            Time.timeScale = Time.timeScale == 1f ? 0f : 1f;
            pauseScreen.SetActive(!pauseScreen.activeSelf);
            curMenu = pauseScreen.activeSelf==true ? pauseScreen : null;
        }

        base.OpenMenu(obj);
    }

    private void ShowGameOverMenu()
    {
        gameoverScreen.SetActive(true);
        curMenu = gameoverScreen;
    }

    private void ShowBestScoreMenu()
    {
        bestscoreScreen.SetActive(true);
        curMenu = bestscoreScreen;
        prevMenu = gameoverScreen;
    }
    public void SetPlayerResult()
    {
        MainManager.Instance.SetCurPlayerScore();
        MainManager.Instance.SavePlayerData();
    }
}
