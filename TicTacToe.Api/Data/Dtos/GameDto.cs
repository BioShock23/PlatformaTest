namespace TicTacToe.Api.Data.Dtos;

public sealed record GameDto
{
    public required Guid Id { get; init; }
    
    public required PlayerDto PlayerOne { get; init; }
    
    public required PlayerDto PlayerTwo { get; init; }
    
    public required Guid? WinnerId { get; set; }
    
    public required Guid? PlayerTurnId { get; set; }
    
    public required bool IsFinished { get; set; }
}
