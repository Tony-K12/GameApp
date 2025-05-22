using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinTapGameManager : MonoBehaviour
{
    public static CoinTapGameManager Instance;

    [Header("UI References")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject endPanel;

    [Header("Gameplay")]
    [SerializeField] private RectTransform spawnArea;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button menuButton1;
    [SerializeField] private Button menuButton2;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button muteButton;
    
    [SerializeField] private Image muteIcon;
    [SerializeField] private Sprite mutedSprite;
    [SerializeField] private Sprite unmutedSprite;

    private int score = 0;
    private float timer = 30f;
    private bool isGameRunning = false;
    private bool isPaused = false;
    private bool isMuted;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        AudioManager.Instance?.PlayMusic(AudioManager.Instance.coinGameMusicClip); //Start to play game BGM and checks if muted
        isMuted = AudioManager.Instance.IsMuted();
        muteIcon.sprite = isMuted ? mutedSprite : unmutedSprite;

        score = 0;
        UpdateScoreUI();
        AssignButtons();
        StartCoroutine(GameLoop());
    }

    //Assign Listeners to each button
    private void AssignButtons()
    {
        pauseButton.onClick.AddListener(OnPause);
        resumeButton.onClick.AddListener(OnResume);
        menuButton1.onClick.AddListener(OnBackToMenu);
        menuButton2.onClick.AddListener(OnBackToMenu);
        restartButton.onClick.AddListener(OnRestart);
        muteButton.onClick.AddListener(ToggleMuteButton);
    }

    //Game state from running, paused to game end
    IEnumerator GameLoop()
    {
        isGameRunning = true;
        while (timer > 0f)
        {
            if (!isPaused)
            {
                timer -= Time.deltaTime;
                timerText.text = $"Time: {Mathf.CeilToInt(timer)}";
            }
            yield return null;
        }
        EndGame();
    }

    //Score calculation
    public void AddScore(int value)
    {
        if (isPaused || !isGameRunning) return;
        score += value;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = $"Score: {score}";
    }

    //Game over
    void EndGame()
    {
        isGameRunning = false;
        endPanel.SetActive(true);
        finalScoreText.text = $"Final Score: {score}";
    }

    public void OnRestart()
    {
        SceneLoader.Instance.LoadScene("CoinGame", endPanel);
        AudioManager.Instance.PlaySFX("button");
    }

    public void OnBackToMenu()
    {
        SceneLoader.Instance.LoadScene("MainMenu", endPanel);
        AudioManager.Instance.PlaySFX("button");
        AudioManager.Instance.StopMusic();
    }

    public void OnPause()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        AudioManager.Instance.PlaySFX("button");
    }

    public void OnResume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        AudioManager.Instance.PlaySFX("button");
    }

    public void ToggleMuteButton()
    {
        AudioManager.Instance.ToggleMute();

        isMuted = AudioManager.Instance.IsMuted();
        muteIcon.sprite = isMuted ? mutedSprite : unmutedSprite;
    }

    public bool IsGameRunning() => isGameRunning && !isPaused;
}
