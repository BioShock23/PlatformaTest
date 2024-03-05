using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Abstractions;
using TicTacToe.Api.Data.Contexts;
using TicTacToe.Api.Data.Dtos;
using TicTacToe.Api.Data.Entities;

namespace TicTacToe.Api.Controllers;

[ApiController]
[Route("api/v1/cell")]
public class CellController : ControllerBase
{
    private ICellService CellService { get; init; }
    private IPlayerService PlayerService { get; init; }
    private ApplicationDbContext Context { get; init; }

    public CellController(
        ICellService cellService,
        IPlayerService playerService,
        ApplicationDbContext context)
    {
        CellService = cellService;
        PlayerService = playerService;
        Context = context;
    }

    [Authorize]
    [HttpPut("mark/{playerId:guid}/{cellId:guid}")]
    public IActionResult Mark(Guid playerId, Guid cellId)
    {
        var cell = CellService.Get(cellId);
        var player = PlayerService.Get(playerId);


        if (cell.MarkedBy != null)
            return BadRequest(
                new CellDto
                {
                    Id = cell.Id,
                    Position = cell.Position
                });

        cell.MarkedById = player.Id;

        cell
           .Board
           .Game
           .Moves
           .Add(
                new Move
                {
                    PlayerId = player.Id
                });

        Context.SaveChanges();

        return Ok(
            new CellDto
        {
            Id = cell.Id,
            Position = cell.Position
        });
    }
}
