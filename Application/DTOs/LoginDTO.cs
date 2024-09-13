/// <summary>
/// Data Transfer Object used for login credentials.
/// </summary>
public class LoginDTO
{
    /// <summary>
    /// Username used for login.
    /// </summary>
    /// <example>admin</example>
    public string Username { get; set; }

    /// <summary>
    /// Password used for authentication.
    /// </summary>
    /// <example>admin</example>
    public string Password { get; set; }
}
