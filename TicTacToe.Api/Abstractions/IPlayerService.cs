using TicTacToe.Api.Data.Entities;

namespace TicTacToe.Api.Abstractions;

public interface IPlayerService
{
    Player Get(Guid id);
}
