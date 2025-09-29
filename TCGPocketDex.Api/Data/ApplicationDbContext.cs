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
    public DbSet<CardTranslation> CardTranslations => Set<CardTranslation>();
    public DbSet<CardSet> CardSets => Set<CardSet>();
    public DbSet<CardSetTranslation> CardSetTranslations => Set<CardSetTranslation>();
    public DbSet<CardRarity> CardRarities => Set<CardRarity>();

    public DbSet<CardPokemon> CardPokemons => Set<CardPokemon>();
    public DbSet<CardSupporter> CardSupporters => Set<CardSupporter>();
    public DbSet<CardFossil> CardFossils => Set<CardFossil>();
    public DbSet<CardItem> CardItems => Set<CardItem>();
    public DbSet<CardTool> CardTools => Set<CardTool>();

    public DbSet<PokemonType> PokemonTypes => Set<PokemonType>();
    public DbSet<PokemonTypeTranslation> PokemonTypeTranslations => Set<PokemonTypeTranslation>();
    public DbSet<PokemonAbility> PokemonAbilities => Set<PokemonAbility>();
    public DbSet<PokemonAbilityTranslation> PokemonAbilityTranslations => Set<PokemonAbilityTranslation>();
    public DbSet<PokemonAttack> PokemonAttacks => Set<PokemonAttack>();
    public DbSet<PokemonAttackTranslation> PokemonAttackTranslations => Set<PokemonAttackTranslation>();

    #endregion

    #region DbContext

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(_schema);

        // Card -> Rarity (required)
        modelBuilder.Entity<Card>()
            .HasOne(c => c.Rarity)
            .WithMany()
            .HasForeignKey(c => c.CardRarityId)
            .OnDelete(DeleteBehavior.Restrict);

        // Card -> CardSet (optional)
        modelBuilder.Entity<Card>()
            .HasOne(c => c.CardSet)
            .WithMany(s => s.Cards)
            .HasForeignKey(c => c.CardSetId)
            .OnDelete(DeleteBehavior.SetNull);

        // Card -> Translations
        modelBuilder.Entity<CardTranslation>()
            .HasOne(t => t.Card)
            .WithMany(c => c.Translations)
            .HasForeignKey(t => t.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        // CardSet -> Translations
        modelBuilder.Entity<CardSetTranslation>()
            .HasOne(t => t.CardSet)
            .WithMany(s => s.Translations)
            .HasForeignKey(t => t.CardSetId)
            .OnDelete(DeleteBehavior.Cascade);

        // Shared PK one-to-one subtypes
        modelBuilder.Entity<CardPokemon>()
            .HasKey(p => p.CardId);
        modelBuilder.Entity<CardPokemon>()
            .HasOne(p => p.Card)
            .WithOne(c => c.Pokemon)
            .HasForeignKey<CardPokemon>(p => p.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CardSupporter>()
            .HasKey(p => p.CardId);
        modelBuilder.Entity<CardSupporter>()
            .HasOne(p => p.Card)
            .WithOne(c => c.Supporter)
            .HasForeignKey<CardSupporter>(p => p.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CardFossil>()
            .HasKey(p => p.CardId);
        modelBuilder.Entity<CardFossil>()
            .HasOne(p => p.Card)
            .WithOne(c => c.Fossil)
            .HasForeignKey<CardFossil>(p => p.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CardItem>()
            .HasKey(p => p.CardId);
        modelBuilder.Entity<CardItem>()
            .HasOne(p => p.Card)
            .WithOne(c => c.Item)
            .HasForeignKey<CardItem>(p => p.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CardTool>()
            .HasKey(p => p.CardId);
        modelBuilder.Entity<CardTool>()
            .HasOne(p => p.Card)
            .WithOne(c => c.Tool)
            .HasForeignKey<CardTool>(p => p.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        // CardPokemon relationships
        // Stage is now an enum; no relationship configuration needed

        modelBuilder.Entity<CardPokemon>()
            .HasOne(p => p.Type)
            .WithMany()
            .HasForeignKey(p => p.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CardPokemon>()
            .HasOne(p => p.Weakness)
            .WithMany()
            .HasForeignKey(p => p.WeaknessTypeId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<CardPokemon>()
            .HasOne(p => p.Ability)
            .WithMany()
            .HasForeignKey(p => p.PokemonAbilityId)
            .OnDelete(DeleteBehavior.SetNull);

        // PokemonAttack translations
        modelBuilder.Entity<PokemonAttackTranslation>()
            .HasOne(t => t.PokemonAttack)
            .WithMany(a => a.Translations)
            .HasForeignKey(t => t.PokemonAttackId)
            .OnDelete(DeleteBehavior.Cascade);

        // PokemonAbility translations
        modelBuilder.Entity<PokemonAbilityTranslation>()
            .HasOne(t => t.PokemonAbility)
            .WithMany(a => a.Translations)
            .HasForeignKey(t => t.PokemonAbilityId)
            .OnDelete(DeleteBehavior.Cascade);

        // PokemonType translations
        modelBuilder.Entity<PokemonTypeTranslation>()
            .HasOne(t => t.PokemonType)
            .WithMany(tn => tn.Translations)
            .HasForeignKey(t => t.PokemonTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Many-to-many: PokemonAttack.Costs <-> PokemonType
        modelBuilder.Entity<PokemonAttack>()
            .HasMany(a => a.Costs)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "PokemonAttackCost",
                right => right.HasOne<PokemonType>().WithMany().HasForeignKey("PokemonTypeId").OnDelete(DeleteBehavior.Cascade),
                left => left.HasOne<PokemonAttack>().WithMany().HasForeignKey("PokemonAttackId").OnDelete(DeleteBehavior.Cascade),
                join => join.ToTable("PokemonAttackCosts", _schema)
            );
    }

    #endregion
}