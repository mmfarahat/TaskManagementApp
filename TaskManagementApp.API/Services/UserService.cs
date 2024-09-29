using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementApp.Application.Contracts;
using TaskManagementApp.Application.Features.Accounts.Queries.GetAllUsers;
using TaskManagementApp.Application.Models;
using TaskManagementApp.Infrastructure.Identity;
namespace TaskManagementApp.API.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        IConfiguration _configuration;
        public UserService(IHttpContextAccessor httpContextAccessor,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _contextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public string LoggedInUserId
        {
            get
            {
                return _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            }
        }

        public async Task<List<UsersListDTO>> GetAllUsers()
        {
            var users = await _userManager.Users.Select(u => new UsersListDTO
            {
                Id = u.Id,
                Email = u.Email
            }).ToListAsync();

            return users;
        }

        public async Task<string> GetUserName(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return user.Email;
            }
            return string.Empty;
        }

        public async Task<TokenResponse?> LogUserIn(string email, string password)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return new TokenResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };
            }
            return null;
        }

        public async Task<bool> VerifyUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            return true;

        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }


    }
}
