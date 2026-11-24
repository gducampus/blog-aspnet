using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Models;

namespace Blog.Services;

public class ArticleService : IArticleService
{
    private readonly BlogContext _context;

    public ArticleService(BlogContext context)
    {
        _context = context;
    }

    public List<Article> GetAll()
    {
        return _context.Articles
            .Include(a => a.Auteur)
            .Include(a => a.Tags)
            .OrderByDescending(a => a.DatePublication)
            .ToList();
    }

    public List<Article> Rechercher(string? recherche, string? tag)
    {
        var requete = _context.Articles
            .Include(a => a.Auteur)
            .Include(a => a.Tags)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(recherche))
        {
            requete = requete.Where(a => a.Titre.ToLower().Contains(recherche.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(tag))
        {
            requete = requete.Where(a => a.Tags.Any(t => t.Nom == tag));
        }

        return requete
            .OrderByDescending(a => a.DatePublication)
            .ToList();
    }

    public List<StatAuteur> GetStatistiques()
    {
        return _context.Auteurs
            .Select(a => new StatAuteur
            {
                Nom = a.Nom,
                NombreArticles = a.Articles.Count,
                DernierArticle = a.Articles.Max(ar => (DateTime?)ar.DatePublication)
            })
            .OrderByDescending(s => s.NombreArticles)
            .ToList();
    }

    public Article? GetById(int id)
    {
        return _context.Articles
            .Include(a => a.Auteur)
            .Include(a => a.Commentaires.OrderBy(c => c.Date))
            .Include(a => a.Tags)
            .FirstOrDefault(a => a.Id == id);
    }

    public void Create(Article article, int[] tagIds)
    {
        article.Tags = _context.Tags.Where(t => tagIds.Contains(t.Id)).ToList();
        _context.Articles.Add(article);
        _context.SaveChanges();
    }

    public void Update(Article article, int[] tagIds)
    {
        var existant = _context.Articles
            .Include(a => a.Tags)
            .First(a => a.Id == article.Id);
        existant.Titre = article.Titre;
        existant.Resume = article.Resume;
        existant.DatePublication = article.DatePublication;
        existant.Tags = _context.Tags.Where(t => tagIds.Contains(t.Id)).ToList();
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var article = _context.Articles.Find(id);
        if (article != null)
        {
            _context.Articles.Remove(article);
            _context.SaveChanges();
        }
    }

    public List<Auteur> GetAuteurs()
    {
        return _context.Auteurs.OrderBy(a => a.Nom).ToList();
    }

    public void AjouterCommentaire(Commentaire commentaire)
    {
        commentaire.Date = DateTime.Now;
        _context.Commentaires.Add(commentaire);
        _context.SaveChanges();
    }

    public List<Tag> GetTags()
    {
        return _context.Tags.OrderBy(t => t.Nom).ToList();
    }

    public Auteur GetOuCreerAuteur(string email)
    {
        var auteur = _context.Auteurs.FirstOrDefault(a => a.Email == email);
        if (auteur == null)
        {
            auteur = new Auteur { Nom = email.Split('@')[0], Email = email };
            _context.Auteurs.Add(auteur);
            _context.SaveChanges();
        }
        return auteur;
    }
}
