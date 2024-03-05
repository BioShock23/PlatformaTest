namespace TicTacToe.Api.Data.Dtos;

public sealed record PlayerDto
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public required Guid GameId { get; init; }

    public required Symbol Side { get; init; }

    public required int Score { get; init; }
}
