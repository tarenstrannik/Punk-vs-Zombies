using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI scoreText;


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

}
