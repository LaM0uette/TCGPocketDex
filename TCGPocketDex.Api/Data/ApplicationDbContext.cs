using Microsoft.EntityFrameworkCore;
using TCGPocketDex.Api.Entities;

namespace TCGPocketDex.Api.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    #region Statements

    private const string _schemaData = "data";
    private const string _schemaRef = "ref";
    private const string _schemaTranslation = "translation";

    #endregion

    #region DbSets

    public DbSet<Card> Cards => Set<Card>();
    public DbSet<CardTranslation> CardTranslations => Set<CardTranslation>();
    public DbSet<CardCollection> CardSets => Set<CardCollection>();
    public DbSet<CardCollectionTranslation> CardSetTranslations => Set<CardCollectionTranslation>();
    public DbSet<CardRarity> CardRarities => Set<CardRarity>();
    public DbSet<CardRarityTranslation> CardRarityTranslations => Set<CardRarityTranslation>();

    public DbSet<CardType> CardTypes => Set<CardType>();
    public DbSet<CardTypeTranslation> CardTypeTranslations => Set<CardTypeTranslation>();
    public DbSet<CardSpecial> CardSpecials => Set<CardSpecial>();
    public DbSet<CardSpecialTranslation> CardSpecialTranslations => Set<CardSpecialTranslation>();

    public DbSet<CardPokemon> CardPokemons => Set<CardPokemon>();
    public DbSet<CardSupporter> CardSupporters => Set<CardSupporter>();
    public DbSet<CardStadium> CardStadiums => Set<CardStadium>();
    public DbSet<CardFossil> CardFossils => Set<CardFossil>();
    public DbSet<CardItem> CardItems => Set<CardItem>();
    public DbSet<CardTool> CardTools => Set<CardTool>();

    public DbSet<PokemonStage> PokemonStages => Set<PokemonStage>();
    public DbSet<PokemonStageTranslation> PokemonStageTranslations => Set<PokemonStageTranslation>();

    public DbSet<PokemonSpecial> PokemonSpecials => Set<PokemonSpecial>();
    public DbSet<PokemonSpecialTranslation> PokemonSpecialTranslations => Set<PokemonSpecialTranslation>();
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

        // Map entities to specific schemas
        modelBuilder.Entity<Card>().ToTable(nameof(Card), _schemaData);
        modelBuilder.Entity<CardTranslation>().ToTable(nameof(CardTranslation), _schemaTranslation);
        modelBuilder.Entity<CardCollection>().ToTable(nameof(CardCollection), _schemaData);
        modelBuilder.Entity<CardCollectionTranslation>().ToTable(nameof(CardCollectionTranslation), _schemaTranslation);
        modelBuilder.Entity<CardRarity>().ToTable(nameof(CardRarity), _schemaRef);
        modelBuilder.Entity<CardRarityTranslation>().ToTable(nameof(CardRarityTranslation), _schemaTranslation);

        modelBuilder.Entity<CardType>().ToTable(nameof(CardType), _schemaRef);
        modelBuilder.Entity<CardTypeTranslation>().ToTable(nameof(CardTypeTranslation), _schemaTranslation);
        modelBuilder.Entity<CardSpecial>().ToTable(nameof(CardSpecial), _schemaRef);
        modelBuilder.Entity<CardSpecialTranslation>().ToTable(nameof(CardSpecialTranslation), _schemaTranslation);

        modelBuilder.Entity<CardPokemon>().ToTable(nameof(CardPokemon), _schemaData);
        modelBuilder.Entity<CardSupporter>().ToTable(nameof(CardSupporter), _schemaData);
        modelBuilder.Entity<CardStadium>().ToTable(nameof(CardStadium), _schemaData);
        modelBuilder.Entity<CardFossil>().ToTable(nameof(CardFossil), _schemaData);
        modelBuilder.Entity<CardItem>().ToTable(nameof(CardItem), _schemaData);
        modelBuilder.Entity<CardTool>().ToTable(nameof(CardTool), _schemaData);

        modelBuilder.Entity<PokemonStage>().ToTable(nameof(PokemonStage), _schemaRef);
        modelBuilder.Entity<PokemonStageTranslation>().ToTable(nameof(PokemonStageTranslation), _schemaTranslation);

        modelBuilder.Entity<PokemonSpecial>().ToTable(nameof(PokemonSpecial), _schemaRef);
        modelBuilder.Entity<PokemonSpecialTranslation>().ToTable(nameof(PokemonSpecialTranslation), _schemaTranslation);
        modelBuilder.Entity<PokemonType>().ToTable(nameof(PokemonType), _schemaRef);
        modelBuilder.Entity<PokemonTypeTranslation>().ToTable(nameof(PokemonTypeTranslation), _schemaTranslation);
        modelBuilder.Entity<PokemonAbility>().ToTable(nameof(PokemonAbility), _schemaData);
        modelBuilder.Entity<PokemonAbilityTranslation>().ToTable(nameof(PokemonAbilityTranslation), _schemaTranslation);
        modelBuilder.Entity<PokemonAttack>().ToTable(nameof(PokemonAttack), _schemaData);
        modelBuilder.Entity<PokemonAttackTranslation>().ToTable(nameof(PokemonAttackTranslation), _schemaTranslation);

        // Card -> Rarity (required)
        modelBuilder.Entity<Card>()
            .HasOne(c => c.Rarity)
            .WithMany()
            .HasForeignKey(c => c.CardRarityId)
            .OnDelete(DeleteBehavior.Restrict);

        // Card -> Type (required)
        modelBuilder.Entity<Card>()
            .HasOne(c => c.Type)
            .WithMany()
            .HasForeignKey(c => c.CardTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Card -> CardSet (optional)
        modelBuilder.Entity<Card>()
            .HasOne(c => c.Collection)
            .WithMany(s => s.Cards)
            .HasForeignKey(c => c.CardCollectionId)
            .OnDelete(DeleteBehavior.SetNull);

        // Card -> Translations
        modelBuilder.Entity<CardTranslation>()
            .HasOne(t => t.Card)
            .WithMany(c => c.Translations)
            .HasForeignKey(t => t.CardId)
            .OnDelete(DeleteBehavior.Cascade);

        // CardSet -> Translations
        modelBuilder.Entity<CardCollectionTranslation>()
            .HasOne(t => t.Collection)
            .WithMany(s => s.Translations)
            .HasForeignKey(t => t.CardCollectionId)
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

        modelBuilder.Entity<CardStadium>()
            .HasKey(p => p.CardId);
        modelBuilder.Entity<CardStadium>()
            .HasOne(p => p.Card)
            .WithOne(c => c.Stadium)
            .HasForeignKey<CardStadium>(p => p.CardId)
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
        modelBuilder.Entity<CardPokemon>()
            .HasOne(p => p.Stage)
            .WithMany()
            .HasForeignKey(p => p.PokemonStageId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CardPokemon>()
            .HasOne(p => p.Type)
            .WithMany()
            .HasForeignKey(p => p.PokemonTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CardPokemon>()
            .HasOne(p => p.Weakness)
            .WithMany()
            .HasForeignKey(p => p.WeaknessPokemonTypeId)
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

        // PokemonStage translations
        modelBuilder.Entity<PokemonStageTranslation>()
            .HasOne(t => t.PokemonStage)
            .WithMany(s => s.Translations)
            .HasForeignKey(t => t.PokemonStageId)
            .OnDelete(DeleteBehavior.Cascade);

        // PokemonType translations
        modelBuilder.Entity<PokemonTypeTranslation>()
            .HasOne(t => t.PokemonType)
            .WithMany(tn => tn.Translations)
            .HasForeignKey(t => t.PokemonTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        // PokemonSpecial translations
        modelBuilder.Entity<PokemonSpecialTranslation>()
            .HasOne(t => t.PokemonSpecial)
            .WithMany(s => s.Translations)
            .HasForeignKey(t => t.PokemonSpecialId)
            .OnDelete(DeleteBehavior.Cascade);

        // CardType translations
        modelBuilder.Entity<CardTypeTranslation>()
            .HasOne(t => t.CardType)
            .WithMany(ct => ct.Translations)
            .HasForeignKey(t => t.CardTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        // CardRarity translations
        modelBuilder.Entity<CardRarityTranslation>()
            .HasOne(t => t.CardRarity)
            .WithMany(r => r.Translations)
            .HasForeignKey(t => t.CardRarityId)
            .OnDelete(DeleteBehavior.Cascade);

        // CardSpecial translations
        modelBuilder.Entity<CardSpecialTranslation>()
            .HasOne(t => t.CardSpecial)
            .WithMany(s => s.Translations)
            .HasForeignKey(t => t.CardSpecialId)
            .OnDelete(DeleteBehavior.Cascade);

        // Many-to-many: PokemonAttack.Costs <-> PokemonType
        modelBuilder.Entity<PokemonAttack>()
            .HasMany(a => a.Costs)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "PokemonAttackCost",
                right => right.HasOne<PokemonType>().WithMany().HasForeignKey("PokemonTypeId").OnDelete(DeleteBehavior.Cascade),
                left => left.HasOne<PokemonAttack>().WithMany().HasForeignKey("PokemonAttackId").OnDelete(DeleteBehavior.Cascade),
                join => join.ToTable("PokemonAttackCosts", _schemaData)
            );

        // Many-to-many: Card.Specials <-> CardSpecial
        modelBuilder.Entity<Card>()
            .HasMany(c => c.Specials)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "CardCardSpecial",
                right => right.HasOne<CardSpecial>().WithMany().HasForeignKey("CardSpecialId").OnDelete(DeleteBehavior.Cascade),
                left => left.HasOne<Card>().WithMany().HasForeignKey("CardId").OnDelete(DeleteBehavior.Cascade),
                join => join.ToTable("CardCardSpecials", _schemaData)
            );

        // Many-to-many: CardPokemon.Specials <-> PokemonSpecial
        modelBuilder.Entity<CardPokemon>()
            .HasMany(p => p.Specials)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "CardPokemonPokemonSpecial",
                right => right.HasOne<PokemonSpecial>().WithMany().HasForeignKey("PokemonSpecialId").OnDelete(DeleteBehavior.Cascade),
                left => left.HasOne<CardPokemon>().WithMany().HasForeignKey("CardId").OnDelete(DeleteBehavior.Cascade),
                join => join.ToTable("CardPokemonPokemonSpecials", _schemaData)
            );

        // Unique indexes on lookup names
        modelBuilder.Entity<CardType>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<CardSpecial>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<PokemonSpecial>().HasIndex(x => x.Name).IsUnique();
    }

    #endregion
}