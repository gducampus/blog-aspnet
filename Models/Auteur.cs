namespace Blog.Models;

public class Auteur
{
    public int Id { get; set; }
    public string Nom { get; set; } = "";
    public string Email { get; set; } = "";

    public List<Article> Articles { get; set; } = new List<Article>();
}
