using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicTacToe.Api.Data.Entities;

public class CellConfiguration : IEntityTypeConfiguration<Cell>
{
    public void Configure(EntityTypeBuilder<Cell> builder)
    {
        builder
           .HasKey(x => x.Id);

        builder
           .HasOne(x => x.MarkedBy)
           .WithMany(x => x.FilledCells)
           .HasForeignKey(x => x.MarkedById);

        builder
           .HasOne(x => x.Board)
           .WithMany(x => x.Cells)
           .HasForeignKey(x => x.BoardId);
    }
}
