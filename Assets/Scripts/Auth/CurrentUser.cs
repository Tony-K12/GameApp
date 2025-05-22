public static class CurrentUser
{

    // Storing curent user data if needed in future

    public static string Phone { get; set; }
    public static string Password { get; set; }

    public static bool IsLoggedIn => !string.IsNullOrEmpty(Phone) && !string.IsNullOrEmpty(Password);

    public static void Clear()
    {
        Phone = "";
        Password = "";
    }
}
