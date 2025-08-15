using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    public float previewTime = 2f;
    public float flipBackDelay = 1f;

    private List<CardView> allCards = new List<CardView>();
    private Queue<CardView> flippedQueue = new Queue<CardView>();
    private bool checkingMatch = false;
    private int turns = 0;
    private int matches = 0;
    private int combo = 0;
    public static event System.Action<int, int, int> OnScoreChanged; 
    private bool lastTurnWasMatch = false; 

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    public void RegisterCard(CardView card)
    {
        if (!allCards.Contains(card))
        {
            allCards.Add(card);
            card.onCardClicked += OnCardClicked;
        }
    }

    public IEnumerator PreviewCards()
    {
        foreach (var card in allCards) card.ShowFront();
        yield return new WaitForSeconds(previewTime);
        foreach (var card in allCards) card.HideFront();
    }

    private void OnCardClicked(CardView clickedCard)
    {
        if (clickedCard.IsFlipped()) return;

        clickedCard.FlipCard(true);
        flippedQueue.Enqueue(clickedCard);
        turns++;
        OnScoreChanged?.Invoke(matches, turns,combo);

        if (!checkingMatch && flippedQueue.Count >= 2)
            StartCoroutine(ProcessMatches());
    }

    private IEnumerator ProcessMatches()
    {
        checkingMatch = true;

        while (flippedQueue.Count >= 2)
        {
            CardView firstCard = flippedQueue.Dequeue();
            CardView secondCard = flippedQueue.Dequeue();

            yield return new WaitForSeconds(flipBackDelay);

            bool match = firstCard.frontImage.sprite == secondCard.frontImage.sprite;

            if (match)
            {
                RegisterMatch(true);
                float matchDuration = 0.3f;
                StartCoroutine(ScaleAndDisable(firstCard, matchDuration));
                StartCoroutine(ScaleAndDisable(secondCard, matchDuration));
            }
            else
            {
                 RegisterMatch(false);
                // Flip back asynchronously
                StartCoroutine(firstCard.FlipCardCoroutine(false));
                StartCoroutine(secondCard.FlipCardCoroutine(false));
            }

            yield return new WaitForSeconds(0.1f);
        }

        checkingMatch = false;
    }

    private IEnumerator ScaleAndDisable(CardView card, float duration)
    {
        Vector3 startScale = card.transform.localScale;
        Vector3 endScale = Vector3.zero;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            card.transform.localScale = Vector3.Lerp(startScale, endScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        card.transform.localScale = endScale;
        card.frontImage.enabled = false;
        card.backImage.enabled = false;
        card.onCardClicked = null;
    }
     public void RegisterMatch(bool isMatch)
    {
        turns++;

        if (isMatch)
        {
            matches++;
            if (lastTurnWasMatch)
            {
                combo++; 
            }
           
            lastTurnWasMatch = true;
        }
        else
        {
            lastTurnWasMatch = false;
        }

        OnScoreChanged?.Invoke(matches, turns, combo);
    }
}
