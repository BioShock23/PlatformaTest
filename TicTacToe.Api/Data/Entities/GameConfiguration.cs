using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicTacToe.Api.Data.Entities;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder
           .HasKey(x => x.Id);

        builder
           .HasOne(x => x.Board)
           .WithOne(x => x.Game);
    }
}
