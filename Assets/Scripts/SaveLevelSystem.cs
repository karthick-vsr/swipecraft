using UnityEngine;

public  class SaveLevelSystem
{
    private const string LEVEL_KEY = "CurrentLevel";

    // Save current level number
    public static void SaveLevel(int levelNumber)
    {
        Debug.Log("level number is " + levelNumber);
        PlayerPrefs.SetInt(LEVEL_KEY, levelNumber);
        PlayerPrefs.Save();
    }

    // Load saved level number, default to 1 if not found
    public static int LoadLevel()
    {
        return PlayerPrefs.GetInt(LEVEL_KEY, 1);
    }

    // Optional: Reset saved progress
    public static void ResetProgress()
    {
        PlayerPrefs.DeleteKey(LEVEL_KEY);
    }
}
