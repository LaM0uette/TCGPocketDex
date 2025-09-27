using Microsoft.EntityFrameworkCore;
using TCGPocketDex.Api.Entity;

namespace TCGPocketDex.Api.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : DbContext(options)
{
    #region Statements

    private readonly string _schema = configuration.GetValue<string>("Schema") ?? "public";

    #endregion

    #region DbSets

    public DbSet<Card> Cards => Set<Card>();
    public DbSet<SupporterCard> SupporterCards => Set<SupporterCard>();
    public DbSet<ItemCard> ItemCards => Set<ItemCard>();
    public DbSet<PokemonToolCard> PokemonToolCards => Set<PokemonToolCard>();
    public DbSet<FossilCard> FossilCards => Set<FossilCard>();
    public DbSet<PokemonCard> PokemonCards => Set<PokemonCard>();

    public DbSet<CardTranslation> CardTranslations => Set<CardTranslation>();

    public DbSet<CardRarity> CardRarities => Set<CardRarity>();

    public DbSet<CardExtension> CardExtensions => Set<CardExtension>();
    public DbSet<CardExtensionTranslation> CardExtensionTranslations => Set<CardExtensionTranslation>();

    public DbSet<Booster> Boosters => Set<Booster>();
    public DbSet<BoosterTranslation> BoosterTranslations => Set<BoosterTranslation>();

    public DbSet<PromoSeries> PromoSeries => Set<PromoSeries>();
    public DbSet<PromoSeriesTranslation> PromoSeriesTranslations => Set<PromoSeriesTranslation>();

    public DbSet<PokemonType> PokemonTypes => Set<PokemonType>();
    public DbSet<PokemonTypeTranslation> PokemonTypeTranslations => Set<PokemonTypeTranslation>();

    public DbSet<PokemonAbility> PokemonAbilities => Set<PokemonAbility>();
    public DbSet<PokemonAbilityTranslation> PokemonAbilityTranslations => Set<PokemonAbilityTranslation>();

    public DbSet<PokemonAttack> PokemonAttacks => Set<PokemonAttack>();
    public DbSet<PokemonAttackTranslation> PokemonAttackTranslations => Set<PokemonAttackTranslation>();

    public DbSet<PokemonStage> PokemonStages => Set<PokemonStage>();
    public DbSet<PokemonStageTranslation> PokemonStageTranslations => Set<PokemonStageTranslation>();

    public DbSet<PokemonWeakness> PokemonWeaknesses => Set<PokemonWeakness>();

    #endregion

    #region DbContext

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(_schema);

        modelBuilder.Entity<Card>().ToTable("Cards");
        modelBuilder.Entity<SupporterCard>().ToTable("SupporterCards");
        modelBuilder.Entity<ItemCard>().ToTable("ItemCards");
        modelBuilder.Entity<PokemonToolCard>().ToTable("PokemonToolCards");
        modelBuilder.Entity<FossilCard>().ToTable("FossilCards");
        modelBuilder.Entity<PokemonCard>().ToTable("PokemonCards");

        modelBuilder.Entity<CardTranslation>()
            .HasIndex(t => new { t.CardId, t.Culture })
            .IsUnique();

        modelBuilder.Entity<PokemonTypeTranslation>()
            .HasIndex(t => new { t.PokemonTypeId, t.Culture })
            .IsUnique();

        modelBuilder.Entity<PokemonAbilityTranslation>()
            .HasIndex(t => new { t.PokemonAbilityId, t.Culture })
            .IsUnique();

        modelBuilder.Entity<PokemonAttackTranslation>()
            .HasIndex(t => new { t.PokemonAttackId, t.Culture })
            .IsUnique();

        modelBuilder.Entity<PokemonStageTranslation>()
            .HasIndex(t => new { t.PokemonStageId, t.Culture })
            .IsUnique();

        modelBuilder.Entity<CardExtensionTranslation>()
            .HasIndex(t => new { t.CardExtensionId, t.Culture })
            .IsUnique();

        modelBuilder.Entity<CardExtension>()
            .HasIndex(e => new { e.Series, e.Code })
            .IsUnique();

        modelBuilder.Entity<Booster>()
            .HasIndex(b => new { b.CardExtensionId })
            .IsUnique();

        modelBuilder.Entity<BoosterTranslation>()
            .HasIndex(t => new { t.BoosterId, t.Culture })
            .IsUnique();

        modelBuilder.Entity<PromoSeries>()
            .HasIndex(p => p.Code)
            .IsUnique();

        modelBuilder.Entity<PromoSeriesTranslation>()
            .HasIndex(t => new { t.PromoSeriesId, t.Culture })
            .IsUnique();

        modelBuilder.Entity<PokemonCard>()
            .HasOne(p => p.Weakness)
            .WithOne(w => w.Pokemon)
            .HasForeignKey<PokemonWeakness>(w => w.PokemonCardId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PokemonCard>()
            .HasOne(p => p.Ability)
            .WithMany()
            .HasForeignKey(p => p.PokemonAbilityId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<PokemonCard>()
            .HasMany(p => p.Attacks)
            .WithMany();

        modelBuilder.Entity<PokemonAttack>()
            .HasMany(a => a.Costs)
            .WithMany(t => t.AttacksUsingType)
            .UsingEntity(j => j.ToTable("PokemonAttackCosts"));

        modelBuilder.Entity<Card>()
            .HasOne(c => c.Booster)
            .WithMany(b => b.Cards)
            .HasForeignKey(c => c.BoosterId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Card>()
            .HasOne(c => c.PromoSeries)
            .WithMany(p => p.Cards)
            .HasForeignKey(c => c.PromoSeriesId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Card>()
            .HasOne(c => c.Extension)
            .WithMany(e => e.Cards)
            .HasForeignKey(c => c.CardExtensionId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Card>()
            .ToTable(t => t.HasCheckConstraint(
                name: "CK_Cards_Source",
                sql: "(\"BoosterId\" IS NOT NULL AND \"PromoSeriesId\" IS NULL AND \"CardExtensionId\" IS NOT NULL) OR (\"BoosterId\" IS NULL AND \"PromoSeriesId\" IS NOT NULL AND \"CardExtensionId\" IS NULL)"));
    }

    #endregion
}