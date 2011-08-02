using DevOne.Security.Cryptography.BCrypt;

public static class PasswordHelpers {
    /// <summary>
    /// returns the given password in blowfish encryption
    /// </summary>
    /// <param name="password">plaintext password</param>
    /// <returns></returns>
    public static string EncryptPassword(string password) {
        return BCryptHelper.HashPassword(password, BCryptHelper.GenerateSalt());
    }
}
