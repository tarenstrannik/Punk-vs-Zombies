using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject pauseScreen;

   private void Start()
    {
        UpdateScore(0);
    }
    private void UpdateHealth(int health)
    {
        healthText.text = $"Health: {health}";
    }
    private void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseAction();
        }
    }
    public void PauseAction()
    {
        Time.timeScale = Time.timeScale == 1f ? 0f : 1f;
        pauseScreen.SetActive(!pauseScreen.activeSelf);
    }
}
