using UnityEngine;
using UnityEngine.UI;

public class EntryManager : MonoBehaviour
{
    [SerializeField] private Button loginButton;
    [SerializeField] private string loginSceneName = "Login";

    private void Start()
    {
        loginButton.onClick.AddListener(OnLoginClicked);
    }

    private void OnLoginClicked()
    {
        AudioManager.Instance.PlaySFX("button");
        SceneLoader.Instance.LoadScene(loginSceneName, loginButton.transform.parent.gameObject);
    }
}
