using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class CardView : MonoBehaviour
{
    public Image frontImage;   
    public Image backImage;    
    private bool isFlipped = false;
    public Action<CardView> onCardClicked;

    void Awake()
    {
        Debug.Log("card view 8****");
        // Start with card hidden (back visible)
        frontImage.enabled = false;
        backImage.enabled = true;
    }
    public void SetFrontSprite(Sprite sprite)
    {
        if (frontImage != null)
        {
            frontImage.sprite = sprite;

        }
    }

    public void ShowFront()
    {
        frontImage.enabled = true;
        backImage.enabled = true;
        isFlipped = true;
    }

    public void HideFront()
    {
        frontImage.enabled = false;
        backImage.enabled = true;
        isFlipped = false;
    }

    public bool IsFlipped()
    {
        return isFlipped;
    }

    public void OnSelectCard()
    {
        Debug.Log("OnCardClicked called****");
        if (!isFlipped)
            onCardClicked?.Invoke(this);
    }
    public void FlipCard(bool showFront)
{
    float duration = 0.25f;

    transform.DORotate(new Vector3(0, 90, 0), duration / 2).OnComplete(() =>
    {
        if (showFront) ShowFront();
        else HideFront();
        transform.DORotate(Vector3.zero, duration / 2);
    });
}
}
