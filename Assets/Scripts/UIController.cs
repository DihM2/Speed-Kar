using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIController : MonoBehaviour
{
    // Menus
    [SerializeField] GameObject mainMenuScreen;
    [SerializeField] GameObject optionsMenuScreen;
    [SerializeField] GameObject recordsMenuScreen;

    // Dropdown for the game difficulty
    [SerializeField] TMP_Dropdown gameModeDropdown;

    // Text to display the Top 3 record
    [SerializeField] TextMeshProUGUI topRecordText;

    // New Game Button clicked
    public void NewGameClicked()
    {
        // Call the new game scene
        SceneManager.LoadScene(1);
    }

    // Options Button Clicked
    public void OptionsButtonClicked()
    {
        mainMenuScreen.SetActive(false);
        optionsMenuScreen.SetActive(true);
    }

    // Records Button Clicked
    public void RecordsButtonClicked()
    {
        mainMenuScreen.SetActive(false);
        recordsMenuScreen.SetActive(true);

        DisplayTopPlayers();
    }

    // Exit button clicked
    public void ExitButtonClicked()
    {
        GameManager.Instance.SaveData();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    // Return button clicked
    public void ReturnButtonClicked()
    {
        // Verify what submenu is active
        if (optionsMenuScreen.activeSelf)
        {
            optionsMenuScreen.SetActive(false);
        }
        else if(recordsMenuScreen.activeSelf)
        {
            recordsMenuScreen.SetActive(false);
        }

        mainMenuScreen.SetActive(true);
    }

    // Change the game difficulty; 0 - easy, 1 - normal, 2 - hard
    public void DifficultyModeChanged()
    {
        //GameManager.Instance.difficultyMode = gameModeDropdown.value;
        GameManager.Instance.difficultyMode = gameModeDropdown.value;
    }

    // Change the text of the Record Display Text
    void DisplayTopPlayers()
    {
        // Tried to use the dictionary, but dont worked
        /*Dictionary<string, int> topRacers = GameManager.Instance.topRacers;

        if(topRacers != null)
        {
            topRecordText.SetText(topRacers.ToString());
        }*/

        
        string[] top3Players = GameManager.Instance.topPlayers;
        int[] top3Score = GameManager.Instance.topScore;
        
        
        if(top3Players != null && top3Score != null)
        {
            // Create the full text to display
            string fullText = "Top Players\n";
            for(int i = 0; i < top3Players.Length; i++)
            {
                fullText += $"{i + 1}. {top3Players[i]} - {top3Score[i]}\n";
            }

            topRecordText.SetText(fullText);
        }

    }
}
