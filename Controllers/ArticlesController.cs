using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Blog.Models;
using Blog.Services;

namespace Blog.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articles;

        public ArticlesController(IArticleService articles)
        {
            _articles = articles;
        }

        // GET: ArticlesController
        public ActionResult Index(string? recherche, string? tag)
        {
            ViewBag.Recherche = recherche;
            ViewBag.Tag = tag;
            return View(_articles.Rechercher(recherche, tag));
        }

        // GET: Articles/Statistiques
        public ActionResult Statistiques()
        {
            return View(_articles.GetStatistiques());
        }

        // GET: Articles/Detail/5
        public ActionResult Detail(int id)
        {
            var article = _articles.GetById(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        private void ChargerAuteurs()
        {
            ViewBag.Auteurs = new SelectList(_articles.GetAuteurs(), "Id", "Nom");
            ViewBag.Tags = _articles.GetTags();
        }

        // GET: Articles/Create
        [Authorize]
        public ActionResult Create()
        {
            ChargerAuteurs();
            return View(new Article { DatePublication = DateTime.Today });
        }

        // POST: Articles/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article, int[] tagIds)
        {
            article.AuteurId = _articles.GetOuCreerAuteur(User.Identity!.Name!).Id;
            ModelState.Remove(nameof(Article.AuteurId));
            if (!ModelState.IsValid)
            {
                ChargerAuteurs();
                return View(article);
            }
            _articles.Create(article, tagIds);
            return RedirectToAction(nameof(Index));
        }

        // GET: Articles/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            var article = _articles.GetById(id);
            if (article == null)
            {
                return NotFound();
            }
            if (!PeutModifier(article))
            {
                return Forbid();
            }
            ChargerAuteurs();
            return View(article);
        }

        // POST: Articles/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Article article, int[] tagIds)
        {
            if (id != article.Id)
            {
                return BadRequest();
            }
            var original = _articles.GetById(id);
            if (original == null)
            {
                return NotFound();
            }
            if (!PeutModifier(original))
            {
                return Forbid();
            }
            ModelState.Remove(nameof(Article.AuteurId));
            if (!ModelState.IsValid)
            {
                ChargerAuteurs();
                return View(article);
            }
            _articles.Update(article, tagIds);
            return RedirectToAction(nameof(Detail), new { id = article.Id });
        }

        // GET: Articles/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var article = _articles.GetById(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _articles.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: Articles/Commenter
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Commenter(Commentaire commentaire)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErreurCommentaire"] = "Indiquez votre nom et un commentaire.";
                return RedirectToAction(nameof(Detail), new { id = commentaire.ArticleId });
            }
            _articles.AjouterCommentaire(commentaire);
            return RedirectToAction(nameof(Detail), new { id = commentaire.ArticleId });
        }

        private bool PeutModifier(Article article)
        {
            return User.IsInRole("Admin") || article.Auteur?.Email == User.Identity?.Name;
        }

    }
}
