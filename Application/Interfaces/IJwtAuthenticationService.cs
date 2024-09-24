namespace motcyApi.Application.Interfaces;

public interface IJwtAuthenticationService
{
    string GenerateToken(string username, string role);
}
