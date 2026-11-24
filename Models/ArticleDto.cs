namespace Blog.Models;

public record ArticleDto(int Id, string Titre, string Resume, DateTime DatePublication, string? Auteur, List<string> Tags);
