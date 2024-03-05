using Microsoft.EntityFrameworkCore;
using TicTacToe.Api.Abstractions;
using TicTacToe.Api.Data.Contexts;
using TicTacToe.Api.Data.Entities;

namespace TicTacToe.Api.Services;

public class GameService : IGameService
{
    private ApplicationDbContext Context { get; init; }

    private List<List<int>> WinningCombinations { get; } =
    [
        [0, 1, 2],
        [0, 3, 6],
        [0, 4, 8],
        [1, 4, 7],
        [2, 4, 6],
        [2, 5, 8],
        [3, 4, 5],
        [6, 7, 8]
    ];

    public GameService(ApplicationDbContext context)
    {
        Context = context;
    }

    public Game Get(Guid id)
    {
        var game = Context
           .Games
           .Include(x => x.Players)
           .Include(x => x.Board)
           .ThenInclude(x => x.Cells)
           .FirstOrDefault(x => x.Id == id);

        return game ?? throw new KeyNotFoundException($"Game with id {id} not found.");
    }


    public Game? GetVacant()
    {
        var game = Context
           .Games
           .FirstOrDefault(x => x.Players.Count < x.MaxPlayers);

        return game;
    }


    public Player? GetNextTurnPlayer(Game game)
    {
        var previous = game
           .Moves
           .MaxBy(x => x.Timestamp);

        if (previous is null)
        {
            return game
               .Players
               .Skip(new Random().Next(1))
               .FirstOrDefault();
        }

        var next = game
           .Players
           .FirstOrDefault(x => x.Id != previous.PlayerId);

        return next;
    }

    public Player GetPlayerOne(Game game)
    {
        var player = game
           .Players
           .FirstOrDefault();

        return player ?? throw new KeyNotFoundException("Could not get player one.");
    }

    public Player GetPlayerTwo(Game game)
    {
        var player = game
           .Players
           .Skip(1)
           .FirstOrDefault();

        return player ?? throw new KeyNotFoundException("Could not get player two.");
    }

    public Player? GetWinner(Game game)
    {
        var board = game.Board;

        if (board is null)
            return null;

        var boxes = board.Cells;

        var winner = (
            from player in game.Players
            let markedPositions = boxes.Where(b => b.MarkedById == player.Id)
               .Select(s => s.Position)
               .ToList()
            let isWinningCombination = WinningCombinations.Any(w => w.All(markedPositions.Contains))
            where isWinningCombination
            select player)
           .FirstOrDefault();
        
        return winner;
    }

    public bool IsFilled(Game game)
    {
        var board = game.Board;

        if (board is null)
            return false;

        return board
           .Cells
           .All(x => x.MarkedById != null);
    }
}
