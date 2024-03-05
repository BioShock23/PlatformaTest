using TicTacToe.Api.Data.Entities;

namespace TicTacToe.Api.Abstractions;

public interface IGameService
{
    Game Get(Guid id);
    
    Game? GetVacant();
    
    Player? GetNextTurnPlayer(Game game);
    
    Player GetPlayerOne(Game game);
    
    Player GetPlayerTwo(Game game);

    Player? GetWinner(Game game);
    
    bool IsFilled(Game game);
}
