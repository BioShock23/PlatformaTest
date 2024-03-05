namespace TicTacToe.Api.Data.Dtos;

public sealed record MoveDto
{
    public required Guid Id { get; init; }
    
    public required DateTime Timestamp { get; init; }
    
    public required Guid PlayerId { get; init; }

    public required GameDto Game { get; init; }
}
