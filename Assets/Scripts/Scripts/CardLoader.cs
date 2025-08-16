using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CardLoader : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;      
    [SerializeField] Transform gridParent;      
    [SerializeField] TextAsset[] levelJson;      
    //public DynamicGrid dynamicGrid;
      

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
            grid.startAxis = GridLayoutGroup.Axis.Horizontal;

            grid.cellSize = new Vector2(96f, 96f);

            RectTransform gridRect = gridParent.GetComponent<RectTransform>();
            float totalWidth = gridRect.rect.width - grid.padding.left - grid.padding.right;
            float totalHeight = gridRect.rect.height - grid.padding.top - grid.padding.bottom;


            float baseSpacing = 20f;
      float spacingX = baseSpacing * Mathf.Max(1f, 4f / level.columns); 
    float spacingY = baseSpacing * Mathf.Max(1f, 4f / level.rows);    

    grid.spacing = new Vector2(spacingX, spacingY);

  
}



    // Clear previous cards
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
