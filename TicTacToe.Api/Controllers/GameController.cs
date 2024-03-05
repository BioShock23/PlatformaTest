using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Abstractions;
using TicTacToe.Api.Data.Dtos;

namespace TicTacToe.Api.Controllers;

[ApiController]
[Route("api/v1/game")]
public sealed class GameController : ControllerBase
{
    private IGameService GameService { get; init; }

    public GameController(IGameService gameService)
    {
        GameService = gameService;
    }

    [HttpGet("{gameId:guid}")]
    public IActionResult Get(Guid gameId)
    {
        var game = GameService.Get(gameId);

        var playerOne = GameService.GetPlayerOne(game);
        var playerTwo = GameService.GetPlayerTwo(game);
        var winner = GameService.GetWinner(game);
        var turn = GameService.GetNextTurnPlayer(game);
        var isFilled = GameService.IsFilled(game);

        if (winner != null)
        {
            winner.Score += 2;
        }

        if (winner != null && isFilled)
        {
            playerOne.Score += 1;
            playerTwo.Score += 1;
        }

        var gameDto = new GameDto
        {
            Id = game.Id,
            PlayerOne = new PlayerDto
            {
                Id = playerOne.Id,
                Name = playerOne.Name,
                GameId = game.Id,
                Side = playerOne.Side,
                Score = playerOne.Score
            },
            PlayerTwo = new PlayerDto
            {
                Id = playerTwo.Id,
                Name = playerTwo.Name,
                GameId = game.Id,
                Side = playerTwo.Side,
                Score = playerTwo.Score
            },
            WinnerId = winner?.Id,
            PlayerTurnId = turn?.Id,
            IsFinished = isFilled
        };

        return Ok(gameDto);
    }
}
