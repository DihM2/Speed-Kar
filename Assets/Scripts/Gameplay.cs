using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gasText;
    [SerializeField] TextMeshProUGUI gameModeText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] TMP_InputField nameInput;

    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject playScreen;
    [SerializeField] GameObject pauseMenuScreen;
    [SerializeField] GameObject gameOverScreen;

    public static Gameplay Instance { get; private set; }

    public bool isGameStart { get; private set; } = false;

    string playerName;
    int score;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        ShowGameMode(GameManager.Instance.difficultyName);
    }

    // Update is called once per frame
    void Update()
    {
        // Game start with [Enter] if the startScreen is active
        if (startScreen.activeSelf && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)))
        {
            GameStart();
        }

        // Pause game with [Esc] or [P] if the startScreen is inactive
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && !startScreen.activeSelf)
        {
            PauseMenu();
        }
    }

    public void GameStart()
    {
        if (nameInput.text.Length != 0)
        {
            playerName = nameInput.text;
            ShowPlayerName();

            startScreen.SetActive(false);
            playScreen.SetActive(true);

            isGameStart = true;
        }
    }

    public void PauseMenu()
    {
        if (isGameStart)
        {
            //Time.timeScale = 0;

            playScreen.SetActive(false);
            pauseMenuScreen.SetActive(true);
        }
        else
        {
            //Time.timeScale = 1.0f;
            playScreen.SetActive(true);
            pauseMenuScreen.SetActive(false);
        }

        isGameStart = !isGameStart;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver(string gameOver)
    {
        isGameStart = false;

        gameOverText.SetText(gameOver);

        playScreen.SetActive(false);
        pauseMenuScreen.SetActive(false);
        gameOverScreen.SetActive(true);

        if (GameManager.Instance.IsHighScore(score))
        {
            // record
            string difficulty = GameManager.Instance.difficultyName;
            GameManager.Instance.UpdateTopPlayers(playerName, score, difficulty);
        }
    }


    void ShowGameMode(string gameMode)
    {
        gameModeText.SetText($"Difficulty:\n{gameMode}");
    }

    void ShowScore(int score)
    {
        scoreText.SetText($"Score: {score}");
    }

    public void ShowFuel(float fuel)
    {
        gasText.SetText($"Gas: {fuel.ToString("f0")}%");
        
        if (fuel < 50)
        {
            gasText.color = Color.red;
        }
        else
        {
            gasText.color = Color.white;
        }
    }

    void ShowPlayerName()
    {
        playerNameText.SetText($"Player:\n{playerName}");
    }

    public void UpdateScore(int value)
    {
        score += value;
        // update Score Text UI
        ShowScore(score);
    }
}
