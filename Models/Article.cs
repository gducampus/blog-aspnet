using System.ComponentModel.DataAnnotations;

namespace Blog.Models;

public class Article
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le titre est obligatoire.")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Le titre doit faire entre 5 et 100 caractères.")]
    public string Titre { get; set; } = "";

    [Required(ErrorMessage = "Le résumé est obligatoire.")]
    [StringLength(500, ErrorMessage = "Le résumé ne doit pas dépasser 500 caractères.")]
    [Display(Name = "Résumé")]
    public string Resume { get; set; } = "";

    [Display(Name = "Date de publication")]
    [DataType(DataType.Date)]
    public DateTime DatePublication { get; set; }

    [Display(Name = "Auteur")]
    public int AuteurId { get; set; }
    public Auteur? Auteur { get; set; }

    public List<Commentaire> Commentaires { get; set; } = new List<Commentaire>();
    public List<Tag> Tags { get; set; } = new List<Tag>();
}
