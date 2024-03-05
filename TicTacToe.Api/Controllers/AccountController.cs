using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TicTacToe.Api.Controllers;

[ApiController]
[Route("api/v1/account")]
public class AccountController
{
    private UserManager<IdentityUser> UserManager { get; init; }
    private SignInManager<IdentityUser> SignInManager { get; init; }

    public AccountController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        UserManager = userManager;
        SignInManager = signInManager;
    }

    [HttpPost("register/{email}/{password}")]
    public async Task Register(string email, string password)
    {
        var user = new IdentityUser {Email = email, UserName = email};
        await UserManager.CreateAsync(user, password);
        await SignInManager.SignInAsync(user, false);
    }

    [HttpPost("login/{email}/{password}")]
    public async Task Login(string email, string password)
    {
        await SignInManager.PasswordSignInAsync(email, password, true, true);
    }
}
