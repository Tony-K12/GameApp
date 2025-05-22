using System.Collections.Generic;

[System.Serializable]
public class UserData
{
    public string phone;
    public string password;
}

public static class DummyUserDatabase
{
    public static List<UserData> users = new List<UserData>() {
        new UserData { phone = "1234567890", password = "pass123" },
        new UserData { phone = "9876543210", password = "test456" }
    };
}