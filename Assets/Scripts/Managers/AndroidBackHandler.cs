using UnityEngine;
using UnityEngine.SceneManagement;

public class AndroidBackHandler : MonoBehaviour
{
    public static AndroidBackHandler Instance;

    public GameObject popupPanel;
    public bool promptBeforeExit = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string currentScene = SceneManager.GetActiveScene().name;

            switch (currentScene)
            {
                case "Entry":
                    Exit();
                    break;

                case "Login":
                    SceneLoader.Instance.LoadScene("Entry", null);
                    break;

                case "MainMenu":
                    SceneLoader.Instance.LoadScene("Login", null);
                    break;

                case "CoinGame":
                    FindAnyObjectByType<CoinTapGameManager>()?.OnPause();
                    break;

                default:
                    SceneLoader.Instance.LoadScene("Entry", null);
                    break;
            }
        }
    }

    void Exit()
    {
        if (!promptBeforeExit)
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            return;
        }

        popupPanel.SetActive(true);

        // TODO: Trigger a UI confirmation panel here
        Debug.Log("Show exit confirmation panel here.");
    }
}
