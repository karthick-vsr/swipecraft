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
         StartCoroutine(FlipCardCoroutine(true));
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

public IEnumerator FlipCardCoroutine(bool showFront, float duration = 0.25f)
{
    float halfDuration = duration / 2f;

    // Rotate Y 0 → 90°
    for (float t = 0; t < halfDuration; t += Time.deltaTime)
    {
        float angle = Mathf.Lerp(0, 90, t / halfDuration);
        transform.localRotation = Quaternion.Euler(0, angle, 0);
        yield return null;
    }

    // Swap front/back
    if (showFront) ShowFront();
    else HideFront();

    // Rotate Y 90 → 0°
    for (float t = 0; t < halfDuration; t += Time.deltaTime)
    {
        float angle = Mathf.Lerp(90, 0, t / halfDuration);
        transform.localRotation = Quaternion.Euler(0, angle, 0);
        yield return null;
    }
}

}
