using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class CardData
{
    public string id;
    public string fruit;
}

[System.Serializable]
public class LevelData
{
    public int level;
    public int rows;
    public int columns;
    public CardData[] cards;
}

