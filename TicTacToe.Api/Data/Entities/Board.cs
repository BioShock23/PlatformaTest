namespace TicTacToe.Api.Data.Entities;

public class Board
{
    public Guid Id { get; set; }
    
    public Guid GameId { get; set; }

    public virtual Game Game { get; set; } = default!;

    public virtual ICollection<Cell> Cells { get; set; } = new List<Cell>();
}