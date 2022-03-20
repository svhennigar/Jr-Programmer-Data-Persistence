using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIEventManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI playerNameInputField;

    // Start is called before the first frame update
    void Start()
    {
        if (playerNameInputField == null)
        {
            Debug.Log("WARNING: playerNameInputField not initialized.");
        }

        if (DataManager.Instance == null)
        {
            Debug.Log("WARNING: DataManager singleton instance not initialized.");
        }
    }

    public void UpdatePlayerName()
    {
        // Set current player name.
        DataManager.Instance.CurrentPlayerName = playerNameInputField.text.ToString();
        Debug.Log($"Player name: {DataManager.Instance.CurrentPlayerName}.");
    }

    // New Game Button event handler.
    // Loads Main scene.
    public void NewGame()
    {
        // Save the current player name.
        UpdatePlayerName();

        // Load the main scene.
        SceneManager.LoadScene("main");
    }

    // Quit Button event handler.
    // Save high score and exit application.
    public void QuitGame()
    {
        // Save high score.
        DataManager.Instance.SaveHighScore();

        // Exit application.
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif

    }


  
}
