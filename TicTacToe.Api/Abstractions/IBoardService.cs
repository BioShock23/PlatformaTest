using TicTacToe.Api.Data.Entities;

namespace TicTacToe.Api.Abstractions;

public interface IBoardService
{
    public ICollection<Cell> Default();

    public bool IsFilled(Board board);
}
