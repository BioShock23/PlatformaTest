using TicTacToe.Api.Data.Entities;

namespace TicTacToe.Api.Abstractions;

public interface ICellService
{
    Cell Get(Guid id);
}
