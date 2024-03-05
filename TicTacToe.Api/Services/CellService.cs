using Microsoft.EntityFrameworkCore;
using TicTacToe.Api.Abstractions;
using TicTacToe.Api.Data.Contexts;
using TicTacToe.Api.Data.Entities;

namespace TicTacToe.Api.Services;

public class CellService : ICellService
{
    private ApplicationDbContext Context { get; init; }

    public CellService(ApplicationDbContext context)
    {
        Context = context;
    }

    public Cell Get(Guid id)
    {
        var cell = Context
           .Cells
           .Include(x => x.MarkedBy)
           .Include(x => x.Board)
           .ThenInclude(x => x.Game)
           .ThenInclude(x => x.Moves)
           .FirstOrDefault(x => x.Id == id);

        return cell ?? throw new KeyNotFoundException($"Cell with id {id} not found.");
    }
}
