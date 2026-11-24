using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Blog.Models;

namespace Blog.Data;

public class BlogContext : IdentityDbContext<IdentityUser>
{
    public BlogContext(DbContextOptions<BlogContext> options) : base(options)
    {
    }

    public DbSet<Article> Articles { get; set; }
    public DbSet<Auteur> Auteurs { get; set; }
    public DbSet<Commentaire> Commentaires { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Auteur>().HasData(
            new Auteur { Id = 1, Nom = "Léa Martin", Email = "lea@blog.fr" },
            new Auteur { Id = 2, Nom = "Karim Benali", Email = "karim@blog.fr" }
        );

        modelBuilder.Entity<Article>().HasData(
            new Article { Id = 1, Titre = "Bienvenue sur le blog", Resume = "Le premier article de notre blog.", DatePublication = new DateTime(2026, 11, 2), AuteurId = 1 },
            new Article { Id = 2, Titre = "Découvrir ASP.NET Core", Resume = "Pourquoi ce framework est devenu un standard.", DatePublication = new DateTime(2026, 11, 9), AuteurId = 2 },
            new Article { Id = 3, Titre = "Le modèle MVC", Resume = "Modèle, vue, contrôleur : qui fait quoi ?", DatePublication = new DateTime(2026, 11, 16), AuteurId = 1 }
        );

        modelBuilder.Entity<Commentaire>().HasData(
            new Commentaire { Id = 1, NomAuteur = "Sophie", Texte = "Bravo pour ce premier article !", Date = new DateTime(2026, 11, 3), ArticleId = 1 },
            new Commentaire { Id = 2, NomAuteur = "Thomas", Texte = "Très clair, merci.", Date = new DateTime(2026, 11, 10), ArticleId = 2 }
        );

        modelBuilder.Entity<Tag>().HasData(
            new Tag { Id = 1, Nom = "Actualité" },
            new Tag { Id = 2, Nom = "ASP.NET Core" },
            new Tag { Id = 3, Nom = "Débutant" }
        );

        modelBuilder.Entity<Article>()
            .HasMany(a => a.Tags)
            .WithMany(t => t.Articles)
            .UsingEntity(j => j.HasData(
                new { ArticlesId = 1, TagsId = 1 },
                new { ArticlesId = 2, TagsId = 2 },
                new { ArticlesId = 2, TagsId = 3 },
                new { ArticlesId = 3, TagsId = 2 },
                new { ArticlesId = 3, TagsId = 3 }
            ));
    }
}
