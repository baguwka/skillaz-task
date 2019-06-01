using System;
using System.Threading.Tasks;
using LinkShortener.Api.Identity;
using LinkShortener.Api.Model;
using LinkShortener.Api.Repo;
using LinkShortener.Api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Api.Controllers
{
    [ApiController]
    public class LinksViewerController : Controller
    {
        private readonly IHttpContextIdentifier _Identifier;
        private readonly ILinksRepository _LinksRepository;

        public LinksViewerController(
            IHttpContextIdentifier identifier,
            ILinksRepository linksRepository)
        {
            _Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
            _LinksRepository = linksRepository ?? throw new ArgumentNullException(nameof(linksRepository));
        }

        [HttpGet]
        [Route("api/links/")]
        public async Task<IActionResult> GetAllLinks()
        {
            var identity = _Identifier.GetOrCreateIdentity(HttpContext);
            var links = await _LinksRepository.GetAllLinksForIdentityAsync(identity);
            var model = GetLinksResponseModel.FromDomainModel(HttpContext.Request.GetBaseUrl(), links);
            return Json(model);
        }
    }
}