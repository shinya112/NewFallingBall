using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int totalScore = 0;
    public TextMeshProUGUI scoreDisplay;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        totalScore += amount;
        UpdateScoreDisplay();
        Debug.Log($"[GameManager] Total score is now: {totalScore}");
    }

    void UpdateScoreDisplay()
    {
        if (scoreDisplay != null)
        {
            scoreDisplay.text = "Score: " + totalScore;
        }
    }
}