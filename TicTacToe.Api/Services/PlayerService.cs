using TicTacToe.Api.Abstractions;
using TicTacToe.Api.Data.Contexts;
using TicTacToe.Api.Data.Entities;

namespace TicTacToe.Api.Services;

public class PlayerService : IPlayerService
{
    private ApplicationDbContext Context { get; init; }

    public PlayerService(ApplicationDbContext context)
    {
        Context = context;
    }

    public Player Get(Guid id)
    {
        var player = Context
           .Players
           .Find(id);

        return player ?? throw new KeyNotFoundException($"Player with id {id} not found.");
    }
}
