using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuUI : MonoBehaviour
{
    // Menus
    [SerializeField] GameObject mainMenuScreen;
    [SerializeField] GameObject optionsMenuScreen;
    [SerializeField] GameObject recordsMenuScreen;
    [SerializeField] GameObject controlAndCreditsMenuScreen;

    // Dropdown for the game difficulty
    [SerializeField] TMP_Dropdown gameModeDropdown;

    // Text to display the Top 3 record
    [SerializeField] TextMeshProUGUI topRecordText;

    [SerializeField] Slider volumeSlider;

    // New Game Button clicked
    public void NewGameClicked()
    {
        // Call the new game scene
        SceneManager.LoadScene(1);
    }

    // Options Button Clicked
    public void OptionsButtonClicked()
    {
        volumeSlider.value = GameManager.Instance.MusicVolume;
        gameModeDropdown.value = ((int)GameManager.Instance.DifficultyMode);

        mainMenuScreen.SetActive(false);
        optionsMenuScreen.SetActive(true);

        //volumeSlider.value = GameManager.Instance.MusicVolume;
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

    // Change the game difficulty; 0 - easy, 1 - normal, 2 - hard, 3 - very hard
    public void DifficultyModeChanged(int value)
    {
        //GameManager.Instance.difficultyMode = gameModeDropdown.value;
        //GameManager.Instance.DifficultyMode = gameModeDropdown.value;
        GameManager.Instance.DifficultyMode = value;
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
        string[] top3Difficulty = GameManager.Instance.topDifficulty;
        
        
        if(top3Players != null && top3Score != null && top3Difficulty != null)
        {
            // Create the full text to display
            string fullText = null;// = "Top Players\n";
            for(int i = 0; i < top3Players.Length; i++)
            {
                fullText += $"{i + 1}. {top3Players[i]} - {top3Score[i]} - {top3Difficulty[i]}\n";
            }

            topRecordText.SetText(fullText);
        }

    }

    public void ControlsMenu()
    {
        // Return to Options menu
        if (controlAndCreditsMenuScreen.activeSelf)
        {
            controlAndCreditsMenuScreen.SetActive(false);
            optionsMenuScreen.SetActive(true);
        }
        // Show the control menu
        else
        {
            controlAndCreditsMenuScreen.SetActive(true);
            optionsMenuScreen.SetActive(false);
        }
    }
}
