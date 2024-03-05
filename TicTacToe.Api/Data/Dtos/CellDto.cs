namespace TicTacToe.Api.Data.Dtos;

public sealed record CellDto
{
    public required Guid Id { get; init; }
    
    public required int Position { get; init; }
    public PlayerDto? MarkedBy { get; init; }
}
