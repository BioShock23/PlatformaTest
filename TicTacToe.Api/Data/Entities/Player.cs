namespace TicTacToe.Api.Data.Entities;


public class Player
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public Guid GameId { get; set; }
    
    public Symbol Side { get; set; } = Symbol.Cross;

    public int Score { get; set; } = 0;
    
    public virtual Game Game { get; set; } = default!;

    public virtual ICollection<Cell> FilledCells { get; set; } = [];
}
