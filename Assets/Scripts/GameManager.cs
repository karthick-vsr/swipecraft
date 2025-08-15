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

    private List<CardView> allCards = new List<CardView>(); // Private list
    private CardView firstCard, secondCard;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
            return;
        }
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
        foreach (var card in allCards)
            card.ShowFront();

        yield return new WaitForSeconds(previewTime);

        foreach (var card in allCards)
            card.HideFront();
    }

    private void OnCardClicked(CardView clickedCard)
    {
        Debug.Log("on card clicked *****");
        if (clickedCard.IsFlipped()) return;

        clickedCard.ShowFront();

        if (firstCard == null)
        {
            firstCard = clickedCard;
        }
        else
        {
            secondCard = clickedCard;
            StartCoroutine(CheckMatch());
        }
    }

   
   private bool canSelect = true; 

private IEnumerator CheckMatch()
{
    canSelect = false; 

    yield return new WaitForSeconds(flipBackDelay);

    if (firstCard != null && secondCard != null)
    {
        bool match = firstCard.frontImage.sprite == secondCard.frontImage.sprite;

        if (match)
        {
            float matchDuration = 0.3f;

            firstCard.transform.DOScale(Vector3.zero, matchDuration).SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    firstCard.frontImage.enabled = false;
                    firstCard.backImage.enabled = false;
                    firstCard.onCardClicked = null; 
                });

            secondCard.transform.DOScale(Vector3.zero, matchDuration).SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    secondCard.frontImage.enabled = false;
                    secondCard.backImage.enabled = false;
                    secondCard.onCardClicked = null; 
                });

        }
        else
        {
            firstCard.FlipCard(false);
            secondCard.FlipCard(false);
        }
    }

    yield return new WaitForSeconds(0.35f); 
    firstCard = null;
    secondCard = null;

    canSelect = true;
}



}
