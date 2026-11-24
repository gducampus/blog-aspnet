using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Blog.Services;

namespace Blog.Controllers.Api
{
    [Route("api/articles")]
    [ApiController]
    public class ArticlesApiController : ControllerBase
    {
        private readonly IArticleService _articles;

        public ArticlesApiController(IArticleService articles)
        {
            _articles = articles;
        }

        // GET /api/articles
        [HttpGet]
        public List<ArticleDto> GetAll()
        {
            return _articles.GetAll().Select(VersDto).ToList();
        }

        // GET /api/articles/2
        [HttpGet("{id}")]
        public ActionResult<ArticleDto> GetById(int id)
        {
            var article = _articles.GetById(id);
            if (article == null)
            {
                return NotFound();
            }
            return VersDto(article);
        }

        private static ArticleDto VersDto(Article a)
        {
            return new ArticleDto(a.Id, a.Titre, a.Resume, a.DatePublication, a.Auteur?.Nom, a.Tags.Select(t => t.Nom).ToList());
        }
    }
}
