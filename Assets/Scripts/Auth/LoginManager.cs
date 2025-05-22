using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField phoneInput;
    [SerializeField] private TMP_InputField passwordInput;

    [Header("Error Texts")]
    [SerializeField] private TMP_Text phoneErrorText;
    [SerializeField] private TMP_Text passwordErrorText;

    [Header("Buttons")]
    [SerializeField] private Button loginButton;
    [SerializeField] private Button passwordToggleButton;
    [SerializeField] private Sprite showIcon;
    [SerializeField] private Sprite hideIcon;
    private bool passwordVisible = false;

    [Header("Scene")]
    [SerializeField] private string nextScene = "MainMenu";

    private void Start()
    {
        loginButton.onClick.AddListener(HandleLogin);
        passwordToggleButton.onClick.AddListener(TogglePasswordVisibility);
    }

    private void HandleLogin()
    {
        bool valid = true;

        string phone = phoneInput.text.Trim();
        string password = passwordInput.text;

        // Phone verification for 10 digits
        if (!Regex.IsMatch(phone, @"^\d{10}$"))
        {
            phoneErrorText.text = "Enter valid 10-digit phone number.";
            phoneErrorText.gameObject.SetActive(true);
            valid = false;
        }
        else phoneErrorText.gameObject.SetActive(false);

        // Password verification
        if (string.IsNullOrWhiteSpace(password))
        {
            passwordErrorText.text = "Password cannot be empty.";
            passwordErrorText.gameObject.SetActive(true);
            valid = false;
        }
        else passwordErrorText.gameObject.SetActive(false);

        if (!valid) return;

        // Check if user exists in dummy DB
        var user = DummyUserDatabase.users.FirstOrDefault(u => u.phone == phone && u.password == password);

        if (user == null)
        {
            passwordErrorText.text = "Invalid phone or password.";
            passwordErrorText.gameObject.SetActive(true);
            return;
        }

        // Set current user info
        CurrentUser.Phone = user.phone;
        CurrentUser.Password = user.password;

        Debug.Log($"Login successful! Welcome {CurrentUser.Phone}");

        AudioManager.Instance.PlaySFX("button");
        SceneLoader.Instance.LoadScene(nextScene, loginButton.transform.parent.gameObject);
    }

    private void TogglePasswordVisibility()
    {
        passwordVisible = !passwordVisible;
        passwordInput.contentType = passwordVisible ? TMP_InputField.ContentType.Standard : TMP_InputField.ContentType.Password;
        passwordInput.ForceLabelUpdate();
        passwordToggleButton.image.sprite = passwordVisible ? hideIcon : showIcon;
    }
}
