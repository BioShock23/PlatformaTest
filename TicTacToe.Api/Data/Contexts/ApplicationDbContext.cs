using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Api.Data.Entities;

namespace TicTacToe.Api.Data.Contexts;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Player> Players { get; set; } = default!;

    public DbSet<Game> Games { get; set; } = default!;

    public DbSet<Move> Moves { get; set; } = default!;

    public DbSet<Board> Boards { get; set; } = default!;

    public DbSet<Cell> Cells { get; set; } = default!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder
           .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
