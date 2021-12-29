using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Instance for the Game Manager
    public static GameManager Instance { get; private set; }

    //public Dictionary<string, int> topRacers { get; private set; }

    // Top players
    public string[] topPlayers { get; private set; } = new string[3] ;
    public int[] topScore { get; private set; } = new int[3];

    // Difficulty setting: 0 - Easy, 1 - Normal, 2 - Hard
    public int difficultyMode = 1;

    // Awake Method
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadData();
    }

    // Add the new record to the top 3 list
    public void UpdateTopPlayers(string playerName, int newScore)
    {
        for (int index = 0; index < topScore.Length; index++)
        {
            if (newScore > topScore[index])
            {
                // Save the old top player
                string oldName = topPlayers[index];
                int oldScore = topScore[index];

                // Add the new top player to the rank
                topPlayers[index] = playerName;
                topScore[index] = newScore;

                // See if the old top player is still on the top 3
                if (IsHighScore(oldScore))
                {
                    UpdateTopPlayers(oldName, oldScore);
                }

                return;
            }
        }
    }

    // Return true if the score is higher than the top 3
    public bool IsHighScore(int score)
    {
        for (int index = (topScore.Length - 1); index >= 0; index--)
        {
            if (score > topScore[index])
            {
                return true;
            }
        }

        return false;
    }

    [Serializable]
    class MemoryData
    {
        //public Dictionary<string, int> topRecord;
        public string[] top3Names;
        public int[] top3Scores;
    }

    // Record the data on the disk
    public void SaveData()
    {
        MemoryData data = new MemoryData();

        //data.topRecord = topRacers;

        data.top3Names = topPlayers;
        data.top3Scores = topScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savedata.json", json);
    }

    // Load the savefile
    void LoadData()
    {
        string path = Application.persistentDataPath + "/savedata.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            MemoryData data = JsonUtility.FromJson<MemoryData>(json);

            //topRacers = data.topRecord;
            topPlayers = data.top3Names;
            topScore = data.top3Scores;
        }
    }

}
