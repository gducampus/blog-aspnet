using Blog.Models;

namespace Blog.Services;

public interface IArticleService
{
    List<Article> GetAll();
    List<Article> Rechercher(string? recherche, string? tag);
    List<StatAuteur> GetStatistiques();
    Article? GetById(int id);
    void Create(Article article, int[] tagIds);
    void Update(Article article, int[] tagIds);
    void Delete(int id);
    List<Auteur> GetAuteurs();
    Auteur GetOuCreerAuteur(string email);
    List<Tag> GetTags();
    void AjouterCommentaire(Commentaire commentaire);
}
