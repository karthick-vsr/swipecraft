using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CardLoader : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;      
    [SerializeField] Transform gridParent;      
    [SerializeField] TextAsset[] levelJson;      
      

    void Start()
    {
          LoadLevel(GameManager.CurrentLevel-1);
    }

    void LoadLevel(int levelIndex)
    {
        if (levelJson == null || levelJson.Length == 0)
        {
            Debug.LogError("No level JSONs assigned!");
            return;
        }

        if (levelIndex < 0 || levelIndex >= levelJson.Length)
        {
            Debug.LogError("Level index out of range!");
            return;
        }
        TextAsset json = levelJson[levelIndex];
        LevelData level = JsonUtility.FromJson<LevelData>(json.text);
        GameManager.Instance.SetTotalPairs(level.cards.Length / 2);

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
