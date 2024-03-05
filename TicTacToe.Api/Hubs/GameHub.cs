using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace TicTacToe.Api.Hubs;

[Authorize]
public class GameHub : Hub
{
    public async Task UpdateBoard(Guid gameId)
    {
        await Clients
           .Group(gameId.ToString())
           .SendAsync("BoardUpdated");
    }
    
    public async Task JoinMatch(Guid gameId, string playerName)
    {
        var groupName = gameId.ToString();

        await Groups
           .AddToGroupAsync(Context.ConnectionId, groupName);

        await Clients
           .GroupExcept(groupName, Context.ConnectionId)
           .SendAsync("PlayerJoined", $"{playerName} has joined the game.");
    }
    
    public async Task LeaveGame(Guid gameId, string playerName)
    {
        var groupName = gameId.ToString();

        await Groups
           .RemoveFromGroupAsync(Context.ConnectionId, groupName);

        await Clients
           .GroupExcept(groupName, Context.ConnectionId)
           .SendAsync("PlayerLeft", $"{playerName} has left the game.");
    }
}
