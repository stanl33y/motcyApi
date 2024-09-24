namespace motcyApi.Application.DTOs;

/// <summary>
/// Data Transfer Object used for login credentials.
/// </summary>
public class LoginDTO
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginDTO"/> class.
    /// </summary>
    /// <param name="username">Username used for login.</param>
    /// <param name="password">Password used for authentication.</param>
    public LoginDTO(string username, string password)
    {
        Username = username;
        Password = password;
    }

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
