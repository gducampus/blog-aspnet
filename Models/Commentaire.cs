using System.ComponentModel.DataAnnotations;

namespace Blog.Models;

public class Commentaire
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Votre nom est obligatoire.")]
    [StringLength(50)]
    public string NomAuteur { get; set; } = "";

    [Required(ErrorMessage = "Le commentaire est vide.")]
    [StringLength(1000, ErrorMessage = "1000 caractères au maximum.")]
    public string Texte { get; set; } = "";

    public DateTime Date { get; set; }

    public int ArticleId { get; set; }
    public Article? Article { get; set; }
}
