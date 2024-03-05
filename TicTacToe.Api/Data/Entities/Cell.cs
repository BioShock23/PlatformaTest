namespace TicTacToe.Api.Data.Entities;

public class Cell
{
    public Guid Id { get; set; }
    
    public int Position { get; set; }
    
    public Guid BoardId { get; set; }
    
    public Guid? MarkedById { get; set; }

    public virtual Board Board { get; set; } = default!;

    public virtual Player? MarkedBy { get; set; }
}
