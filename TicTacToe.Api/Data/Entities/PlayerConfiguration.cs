using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicTacToe.Api.Data.Entities;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder
           .HasKey(x => x.Id);

        builder
           .Property(x => x.Name)
           .HasMaxLength(20);

        builder
           .HasOne(x => x.Game)
           .WithMany(x => x.Players)
           .HasForeignKey(x => x.GameId);
    }
}
