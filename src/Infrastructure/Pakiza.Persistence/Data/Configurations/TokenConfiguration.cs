namespace Pakiza.Persistence.Data.Configurations;
public class TokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable("Tokens");

        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.User)
               .WithMany()
               .HasForeignKey(t => t.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(t => t.RefreshToken).IsRequired();
        builder.Property(t => t.DateCreated).IsRequired();
        builder.Property(t => t.DateExpired).IsRequired();
        builder.Property(t => t.LifeTime).IsRequired();

        builder.HasIndex(t => t.UserId);
        builder.HasIndex(t => t.RefreshToken).IsUnique();
    }
}
