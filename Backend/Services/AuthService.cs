using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class AuthService
{
    private readonly IConfiguration _config;
    public AuthService(IConfiguration config)
    {
        _config=config;
    }

    public string GenerateToken(User user)
    {
        //header
        //payload
        //secret
        
        var claims = new[] {
          new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
          new Claim(JwtRegisteredClaimNames.Email, user.Email),
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
        if (key == null)
        {
            throw new Exception("JWT Key is not configured.");
        }
        Console.WriteLine($"JWT Key: {key}");
        var cred = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"]
            ,audience:_config["Jwt:Audience"],
            claims:claims,
            expires:DateTime.UtcNow.AddMinutes(30),
            signingCredentials:cred);
        
         return new JwtSecurityTokenHandler().WriteToken(token);
    }
   
}