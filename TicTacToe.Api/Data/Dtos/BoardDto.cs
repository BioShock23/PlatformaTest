namespace TicTacToe.Api.Data.Dtos;

public sealed record BoardDto
{
    public required Guid Id { get; init; }
    
    public required ICollection<CellDto> Cells { get; init; }
}
