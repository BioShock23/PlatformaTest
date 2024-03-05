using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Api.Abstractions;
using TicTacToe.Api.Data;
using TicTacToe.Api.Data.Contexts;
using TicTacToe.Api.Data.Dtos;
using TicTacToe.Api.Data.Entities;

namespace TicTacToe.Api.Controllers;

[ApiController]
[Route("api/v1/player")]
public sealed class PlayerController : ControllerBase
{
    private ApplicationDbContext Context { get; init; }
    private IGameService GameService { get; init; }

    public PlayerController(
        IGameService gameService,
        ApplicationDbContext context)
    {
        GameService = gameService;
        Context = context;
    }

    [Authorize]
    [HttpPost("{name}")]
    public IActionResult Create(string name)
    {
        var game = GameService.GetVacant();

        if (game is null)
        {
            game = new Game();

            Context
               .Games
               .Add(game);

            Context.SaveChanges();
        }

        var newPlayer = new Player
        {
            Name = name,
            Side = game.Players.Count >= 1 ? Symbol.Zero : Symbol.Cross,
            GameId = game.Id,
            Game = game
        };

        Context
           .Games
           .Include(x => x.Players)
           .FirstOrDefault(x => x.Id == game.Id)!
           .Players
           .Add(newPlayer);

        Context.SaveChanges();

        return StatusCode(
            StatusCodes.Status201Created,
            new PlayerDto
            {
                Id = newPlayer.Id,
                Name = newPlayer.Name,
                GameId = newPlayer.GameId,
                Score = newPlayer.Score,
                Side = newPlayer.Side
            });
    }
}
