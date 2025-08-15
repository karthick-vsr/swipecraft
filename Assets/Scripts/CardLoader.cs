using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CardLoader : MonoBehaviour
{
    public GameObject cardPrefab;      
    public Transform gridParent;      
    public TextAsset levelJson;      

    void Start()
    {
        LoadLevel(levelJson.text);
    }

    void LoadLevel(string json)
    {
        LevelData level = JsonUtility.FromJson<LevelData>(json);

        GridLayoutGroup grid = gridParent.GetComponent<GridLayoutGroup>();
        if (grid != null)
        {
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = level.columns;
            grid.startAxis = GridLayoutGroup.Axis.Horizontal; // row-wise
        }

        foreach (Transform child in gridParent)
            Destroy(child.gameObject);

        foreach (var card in level.cards)
        {
            GameObject newCard = Instantiate(cardPrefab, gridParent, false); 

            newCard.name = card.id;

            CardView cardView = newCard.GetComponent<CardView>();
            if (cardView != null)
            {
                Sprite sprite = Resources.Load<Sprite>(card.fruit);
                if (sprite != null)
                    cardView.SetFrontSprite(sprite);

                GameManager.Instance.RegisterCard(cardView);
            }
        }
            StartCoroutine(GameManager.Instance.PreviewCards());



    }
}
