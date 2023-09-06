/*namespace jwtauth;

public class BaseConfigrationSettings<TEntity> : BaseConfigration<TEntity> , IEntityTypeConfiguration<TEntity> where TEntity : BaseEntitySettings
{
    public new virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.Name).HasMaxLength(20);
    }

}*/