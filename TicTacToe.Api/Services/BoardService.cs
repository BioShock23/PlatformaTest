using TicTacToe.Api.Abstractions;
using TicTacToe.Api.Data.Entities;

namespace TicTacToe.Api.Services;

public class BoardService : IBoardService
{
    public ICollection<Cell> Default() =>
    [
        new Cell {Position = 0},
        new Cell {Position = 1},
        new Cell {Position = 2},
        new Cell {Position = 3},
        new Cell {Position = 4},
        new Cell {Position = 5},
        new Cell {Position = 6},
        new Cell {Position = 7},
        new Cell {Position = 8},
    ];

    public bool IsFilled(Board board)
    {
        return board
           .Cells
           .All(x => x.MarkedById != null);
    }
}
