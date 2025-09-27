using Microsoft.EntityFrameworkCore;

namespace TCGPocketDex.Api.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : DbContext(options)
{
    #region Statements

    private readonly string _schema = configuration.GetValue<string>("Schema") ?? "public";

    #endregion

    #region DbContext

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(_schema);
    }

    #endregion
}