using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInGame : UIDisplay
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject pauseScreen;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (scoreText != null)
            UpdateScore(0);

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

    public override void OpenMenu(UnityEngine.Object obj)
    {
        base.OpenMenu(obj);
        GameObject target = obj as GameObject;
        if (target == null && MainManager.Instance.isGameActive)
        {
            Time.timeScale = Time.timeScale == 1f ? 0f : 1f;
            pauseScreen.SetActive(!pauseScreen.activeSelf);
            curMenu = pauseScreen.activeSelf==true ? pauseScreen : null;
        }



    }
}
