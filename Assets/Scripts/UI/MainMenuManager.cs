using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text welcomeText;
    [SerializeField] private Button playGameButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private string gameSceneName = "CoinTapGame";

    private void Start()
    {
        // Set welcome text
        if (CurrentUser.IsLoggedIn)
            welcomeText.text = $"Welcome, {CurrentUser.Phone}!";
        else
            welcomeText.text = $"Welcome, Guest!";

        // Add button listeners
        playGameButton.onClick.AddListener(OnPlayGame);
        exitButton.onClick.AddListener(OnExitApp);
    }

    private void OnPlayGame()
    {
        AudioManager.Instance.PlaySFX("button");
        SceneLoader.Instance.LoadScene(gameSceneName, playGameButton.transform.parent.gameObject);
    }

    private void OnExitApp()
    {
        AudioManager.Instance?.PlaySFX("button");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
