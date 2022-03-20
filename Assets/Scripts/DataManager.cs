using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string CurrentPlayerName {get; set; }       // Current player's name.
    public string HighScorePlayerName { get; set; }    // Name of player with highest score.
    public int HighScore { get; set; }                 // Highest score.

    // Serializable class for persistent
    // storage of high score.
    [System.Serializable]
    public class HighScoreStore
    {
        public string name;
        public int score;

        public HighScoreStore(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }

    private string saveFileName;


    // Awake is called each time script is loaded.
    void Awake()
    {
        // DataManager is a singleton.  If it has
        // already been initialized, destroy this 
        // instance.
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // First time script is loaded.
        // Set static Instance to the current instance of the game object.
        Instance = this;

        // Don't destroy this gameObject instance when loading scenes.
        DontDestroyOnLoad(gameObject);

        // Initialize save file path.
        saveFileName = Application.dataPath + "/" + "highscore.json";

        // Load high score from file.
        LoadHighScore();
    }

    public void LoadHighScore()
    {
        // If there is no save file, set high score to default values.
        if (!File.Exists(saveFileName))
        {
            Debug.Log($"WARNING: High score save file does not exist.  Setting to default.");
            HighScorePlayerName = "";
            HighScore = 0;
            return;
        }

        // Load high score from file.
        using (var inputStream = new StreamReader(saveFileName))
        {
            try
            {
                // Read JSON text
                string jsonText = inputStream.ReadLine();

                // Deserialize
                HighScoreStore hs = JsonUtility.FromJson<HighScoreStore>(jsonText);

                if (hs != null)
                {
                    // Set high score.
                    HighScorePlayerName = hs.name;
                    HighScore = hs.score;
                }
            }
            catch (System.Exception e)
            {
                Debug.Log($"ERROR: Failed to load high score.   Error: {e.Message}");
            }
        }
    }

    public void SaveHighScore()
    {
        // Create a serializable instance of HighScore.
        HighScoreStore score = new HighScoreStore(HighScorePlayerName, HighScore);
        
        // Convert to JSON
        string jsonText = JsonUtility.ToJson(score);

        // Save to file.
        using (var outputStream = new StreamWriter(saveFileName))
        {
            try
            {
                outputStream.Write(jsonText);
                outputStream.Flush();
            }
            catch (System.Exception e)
            {
                Debug.Log($"ERROR: Failed to save high score.   Error: {e.Message}");
            }
        }
    }

    
}
