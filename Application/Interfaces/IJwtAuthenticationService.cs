public interface IJwtAuthenticationService
{
    string GenerateToken(string username, string role);
}
