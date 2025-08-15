using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text level_no;
    // Map difficulties to level numbers

    private void Start()
    {
        
        level_no.text = "Level: " + SaveLevelSystem.LoadLevel();
    }


    public void LoadLevel()
    {

        // Save selected level in GameManager
        GameManager.CurrentLevel = SaveLevelSystem.LoadLevel();

        // Load the Game Scene
        SceneManager.LoadScene("GameScene");
    }
}
