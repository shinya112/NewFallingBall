using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private int currentScore;
    public TextMeshPro scoreText;

    void Start()
    {
        currentScore = 10; // èâä˙ÉXÉRÉA
        UpdateScoreText();
    }

    public int CurrentScore => currentScore;

    public void SetScore(float newScore)
    {
        currentScore = Mathf.RoundToInt(newScore);
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreText();
    }

    public void ApplyModifier(BounceObjectData modifier)
    {
        if (modifier != null)
        {
            float modifiedScore = modifier.ApplyModifier(currentScore);
            SetScore(modifiedScore);
            Debug.Log($"[FallingObject] Score changed by {modifier.GetDisplayText()} Å® New score: {currentScore}");
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BounceObjectData modifier = collision.collider.GetComponent<BounceObjectData>();
        if (modifier != null)
        {
            ApplyModifier(modifier);
        }
    }
}