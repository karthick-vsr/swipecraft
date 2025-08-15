using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Map difficulties to level numbers
    public void StartEasy()
    {

        LoadLevel(1); // Level 1
    }

    public void StartMedium()
    {
        LoadLevel(2); // Level 2
    }

    public void StartHard()
    {
        LoadLevel(3); // Level 3
    }

    void LoadLevel(int levelNumber)
    {
        // Save selected level in GameManager
        GameManager.CurrentLevel = levelNumber;

        // Load the Game Scene
        SceneManager.LoadScene("GameScene");
    }
}
