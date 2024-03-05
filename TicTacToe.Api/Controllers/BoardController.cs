using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Abstractions;
using TicTacToe.Api.Data.Contexts;
using TicTacToe.Api.Data.Dtos;
using TicTacToe.Api.Data.Entities;

namespace TicTacToe.Api.Controllers;

[ApiController]
[Route("api/v1/board")]
public class BoardController : ControllerBase
{
    private IGameService GameService { get; init; }
    private IBoardService BoardService { get; init; }
    private ApplicationDbContext Context { get; init; }

    public BoardController(
        IGameService gameService,
        IBoardService boardService,
        ApplicationDbContext context)
    {
        GameService = gameService;
        BoardService = boardService;
        Context = context;
    }

    [HttpGet("{gameId:guid}")]
    public IActionResult Get(Guid gameId)
    {
        var game = GameService.Get(gameId);

        var board = game.Board;

        if (board is null)
        {
            board = new Board
            {
                GameId = game.Id,
                Game = game,
                Cells = BoardService.Default()
            };

            Context.Boards.Add(board);
            Context.SaveChanges();
        }

        var boxes = game.Board?.Cells;

        var boardDto = new BoardDto
        {
            Id = game.Board.Id,
            Cells = boxes.Select(x => new CellDto
                {
                    Id = x.Id,
                    Position = x.Position,
                    MarkedBy = x.MarkedBy != null
                        ? new PlayerDto
                        {
                            Id = x.MarkedBy.Id,
                            Name = x.MarkedBy.Name,
                            Side = x.MarkedBy.Side,
                            GameId = game.Id,
                            Score = x.MarkedBy.Score
                        }
                        : null
                })
               .ToList()
        };

        return Ok(boardDto);
    }
}
