using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text matchesText;
    public TMP_Text turnsText;

    private void Start()
    {
        GameManager.OnScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        GameManager.OnScoreChanged -= UpdateScore;
    }

    private void UpdateScore(int matches, int turns)
    {
        Debug.Log("update score *****");
        if (matchesText != null) matchesText.text = "Matches: " + matches;
        if (turnsText != null) turnsText.text = "Turns: " + turns;
    }
}
