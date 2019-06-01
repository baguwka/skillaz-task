using System;
using System.Threading.Tasks;
using LinkShortener.Api.Identity;
using LinkShortener.Api.Model;
using LinkShortener.Api.Repo;
using LinkShortener.Api.Repo.DomainModel;
using LinkShortener.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Shortener.Lib;

namespace LinkShortener.Api.Controllers
{
    //todo add error filter
    [ApiController]
    public class LinkShortenerController : Controller
    {
        private readonly IHttpContextIdentifier _Identifier;
        private readonly IShortener _Shortener;
        private readonly ILinksRepository _LinksRepository;

        public LinkShortenerController(
            IHttpContextIdentifier identifier, 
            IShortener shortener, 
            ILinksRepository linksRepository
        )
        {
            _Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
            _Shortener = shortener ?? throw new ArgumentNullException(nameof(shortener));
            _LinksRepository = linksRepository ?? throw new ArgumentNullException(nameof(linksRepository));
        }

        [HttpPost]
        [Route("api/shorten")]
        public async Task<IActionResult> ShortenLink([FromForm] string url)
        {
            var identity = _Identifier.GetOrCreateIdentity(HttpContext);
            var shortenResult = await _Shortener.ShortenAsync(url);

            var linkToAdd = new LinkModel
            {
                OriginalUrl = url,
                ShortenId = shortenResult,
                Identity = identity
            };

            await _LinksRepository.AddLinkAsync(linkToAdd);

            var response = new AddLinkResponseModel
            {
                Identity = identity,
                Shorten = $"{HttpContext.Request.GetBaseUrl()}/l/{shortenResult}",
                Original = url
            };

            return Json(response);
        }
    }
}