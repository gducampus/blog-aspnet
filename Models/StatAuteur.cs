namespace Blog.Models;

public class StatAuteur
{
    public string Nom { get; set; } = "";
    public int NombreArticles { get; set; }
    public DateTime? DernierArticle { get; set; }
}
