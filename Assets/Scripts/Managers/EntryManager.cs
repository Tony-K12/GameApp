using UnityEngine;
using UnityEngine.UI;

public class EntryManager : MonoBehaviour
{
    [SerializeField] private string loginSceneName = "Login";
    [SerializeField] private GameObject exitPopup;
    [SerializeField] private Button exitYesButton;
    [SerializeField] private Button exitNoButton;
    [SerializeField] private Button loginButton;

    private void Start()
    {
        exitYesButton.onClick.AddListener(ExitApp);
        exitNoButton.onClick.AddListener(CloseExitPopup);
        loginButton.onClick.AddListener(OnLoginClicked);

        if (exitPopup != null )
        {
            AndroidBackHandler.Instance.promptBeforeExit = true;
            AndroidBackHandler.Instance.popupPanel = exitPopup;
        }
    }

    private void OnLoginClicked()
    {
        AudioManager.Instance.PlaySFX("button");
        SceneLoader.Instance.LoadScene(loginSceneName, loginButton.transform.parent.gameObject);
    }

    private void ExitApp()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        return;
    }

    private void CloseExitPopup()
    {
        exitPopup.SetActive(false);
    }
}
