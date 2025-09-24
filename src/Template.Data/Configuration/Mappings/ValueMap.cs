using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.AggregatesModel.ValueAggreate;

namespace Template.Data.Configuration.Mappings;

[ExcludeFromCodeCoverage]
public class ValueMap : IEntityTypeConfiguration<Value>
{
    public const int VALUE_CODE_LENGTH = 45;
    public const int VALUE_DESCRIPTION_LENGTH = 120;

    public void Configure(EntityTypeBuilder<Value> builder)
    {
        builder.ToTable("Value");

        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Id)
            .IsRequired()
            .HasColumnName("Id")
            .HasColumnType("uuid")
            .ValueGeneratedNever();

        builder.Property(e => e.Code)
            .HasColumnName("Code")
            .IsUnicode(false)
            .HasMaxLength(VALUE_CODE_LENGTH);

        builder.Property(e => e.Description)
            .IsUnicode(false)
            .HasColumnName("Description")
            .HasMaxLength(VALUE_DESCRIPTION_LENGTH);

        builder.Property(e => e.Status)
            .HasColumnName("Status")
            .HasConversion(
                v => v.ToString(),
                v => (Status)Enum.Parse(typeof(Status), v))
            .HasDefaultValue(Status.Active);

        builder.Property(e => e.Timestamp)
            .HasColumnName("Timestamp")
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
