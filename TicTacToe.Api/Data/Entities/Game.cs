namespace TicTacToe.Api.Data.Entities;

public class Game
{
    public Guid Id { get; set; }

    public int MaxPlayers { get; set; } = 2;

    public virtual Board? Board { get; set; }

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    public virtual ICollection<Move> Moves { get; set; } = new List<Move>();
}
