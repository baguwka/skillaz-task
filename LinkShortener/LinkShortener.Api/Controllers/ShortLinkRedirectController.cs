using System;
using System.Threading.Tasks;
using LinkShortener.Api.Repo;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Api.Controllers
{
    [ApiController]
    public class ShortLinkRedirectController : Controller
    {
        private readonly ILinksRepository _LinksRepository;

        public ShortLinkRedirectController(ILinksRepository linksRepository)
        {
            _LinksRepository = linksRepository ?? throw new ArgumentNullException(nameof(linksRepository));
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/l/{shortenId}")]
        public async Task<IActionResult> RedirectAction(string shortenId)
        {
            var foundLink = await _LinksRepository.GetLinkByShortenIdAndIncCounterAsync(shortenId);
            if (string.IsNullOrWhiteSpace(foundLink?.OriginalUrl))
                return NotFound("Full link not found");

            return Redirect(foundLink.OriginalUrl);
        }
    }
}