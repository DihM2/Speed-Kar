using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField] AudioClip veryHardMusic;
    [SerializeField] Slider volumeSlider;
    AudioSource bgMusic;

    // ENCAPSULATION
    public static Gameplay Instance { get; private set; }

    // ENCAPSULATION
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

        bgMusic = Camera.main.GetComponent<AudioSource>();

        bgMusic.volume = GameManager.Instance.MusicVolume;
        volumeSlider.value = bgMusic.volume;
    }

    // Update is called once per frame
    void Update()
    {
        // Game start with [Enter] if the startScreen is active
        if (startScreen.activeSelf && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)))
        {
            // ABSTRACTION
            GameStart();
        }

        // Pause game with [Esc] or [P] if the startScreen is inactive
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && !startScreen.activeSelf && !gameOverScreen.activeSelf)
        {
            // ABSTRACTION
            PauseMenu();
        }
    }

    // ABSTRACTION
    public void GameStart()
    {
        if (nameInput.text.Length != 0)
        {
            playerName = nameInput.text;
            ShowPlayerName();

            startScreen.SetActive(false);
            playScreen.SetActive(true);

            isGameStart = true;
            if(GameManager.Instance.DifficultyMode == 2f)
            {
                bgMusic.clip = veryHardMusic;
            }
            bgMusic.Play();
        }
    }

    // ABSTRACTION
    public void PauseMenu()
    {
        // Pause
        if (isGameStart)
        {
            //Time.timeScale = 0;

            playScreen.SetActive(false);
            pauseMenuScreen.SetActive(true);
            bgMusic.Pause();


        }
        // Unpause
        else
        {
            //Time.timeScale = 1.0f;
            playScreen.SetActive(true);
            pauseMenuScreen.SetActive(false);
            bgMusic.Play();
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

    // Game Over
    public void GameOver(string gameOver)
    {
        isGameStart = false;

        gameOverText.SetText(gameOver);

        playScreen.SetActive(false);
        pauseMenuScreen.SetActive(false);
        gameOverScreen.SetActive(true);


        // Stop music
        bgMusic.Stop();

        // See if is a new Record
        if (GameManager.Instance.IsHighScore(score))
        {
            string difficulty = GameManager.Instance.difficultyName;
            GameManager.Instance.UpdateTopPlayers(playerName, score, difficulty);
        }
    }

    // Display the current game mode difficulty
    void ShowGameMode(string gameMode)
    {
        gameModeText.SetText($"Difficulty:\n{gameMode}");
    }

    // Display the current score
    void ShowScore(int score)
    {
        scoreText.SetText($"Score: {score}");
    }

    // Display the current fuel
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

    // Display the Player Name
    void ShowPlayerName()
    {
        playerNameText.SetText($"Player:\n{playerName}");
    }

    // Add/remove value of the score and display on the screen
    public void UpdateScore(int value)
    {
        score += value;
        // update Score Text UI
        ShowScore(score);
    }

    public void VolumeChange(float value)
    {
        GameManager.Instance.MusicVolume = value;
    }

    public void DifficultyChange(int value)
    {
        GameManager.Instance.DifficultyMode = value;
    }
}
