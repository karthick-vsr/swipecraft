using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TMP_Text matchesText;
    [SerializeField] TMP_Text turnsText;
    [SerializeField] TMP_Text comobTxt;
   

    private void Start()
    {
        GameManager.OnScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        GameManager.OnScoreChanged -= UpdateScore;
    }

    private void UpdateScore(int matches, int turns, int combo)
    {
        Debug.Log("update score *****");
        if (matchesText != null) matchesText.text = "Matches\n" + matches;
        if (turnsText != null) turnsText.text = "Turns\n" + turns;
        if (comobTxt != null) comobTxt.text = "Combo\n" + combo;
    }
}
