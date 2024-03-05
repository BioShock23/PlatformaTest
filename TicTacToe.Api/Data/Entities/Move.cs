namespace TicTacToe.Api.Data.Entities;

public class Move
{
    public Guid Id { get; set; }
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    public Guid PlayerId { get; set; }

    public virtual Game Game { get; set; } = default!;
}
