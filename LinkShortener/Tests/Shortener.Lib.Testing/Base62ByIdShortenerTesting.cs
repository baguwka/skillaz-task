using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Shortener.Lib.Exceptions;
using Shortener.Lib.Ids;
using Shortener.Lib.Shorten;

namespace Shortener.Lib.Testing
{
    public class Base62ByIdShortenerTesting
    {
        [Test]
        public async Task CheckBase62Conversion()
        {
            var idGenerator = GetMockedLinksIdReturns(2548384);
            var validator = GetAlwaysGoodUrlValidator();

            var shortener = new Base62ByIdLinksShortener(idGenerator, validator);
            var actual = await shortener.ShortenAsync("qwe");

            Assert.That(actual, Is.EqualTo("kQ68"));
        }

        [Test]
        public async Task PassNullUrl_assert_expection_is_thrown()
        {
            var idGenerator = GetMockedLinksIdReturns(2548384);
            var validator = GetAlwaysGoodUrlValidator();

            var shortener = new Base62ByIdLinksShortener(idGenerator, validator);
            Assert.ThrowsAsync<UrlIsMissingException>(async () =>
            {
                var _ = await shortener.ShortenAsync(null); 
            });
        }

        [Test]
        public async Task PassEmptyUrl_assert_expection_is_thrown()
        {
            var idGenerator = GetMockedLinksIdReturns(2548384);
            var validator = GetAlwaysGoodUrlValidator();
            var shortener = new Base62ByIdLinksShortener(idGenerator, validator);
            Assert.ThrowsAsync<UrlIsMissingException>(async () =>
            {
                var _ = await shortener.ShortenAsync(string.Empty);
            });
        }

        public static IEnumerable<TestCaseData> InvalidUrlsCases
        {
            get 
            {
                yield return new TestCaseData("qwe");
                yield return new TestCaseData("htp:asdqw");
                yield return new TestCaseData("htt://asd.ru");
                yield return new TestCaseData("://asd.ru");
                yield return new TestCaseData("asd.ru");
                yield return new TestCaseData("http://asd");
            }
        }

        [TestCaseSource(nameof(InvalidUrlsCases))]
        public async Task PassInvalidUrl_assert_exception_is_thrown(string invalidUrl)
        {
            var idGenerator = GetMockedLinksIdReturns(2548384);
            var shortener = new Base62ByIdLinksShortener(idGenerator, new BclUrlValidator());
            Assert.ThrowsAsync<UrlIsInvalidException>(async () =>
            {
                await shortener.ShortenAsync(invalidUrl);
            });
        }

        public ILinksIdGenerator GetMockedLinksIdReturns(long idToReturn)
        {
            var idGenerator = Mock.Of<ILinksIdGenerator>();
            Mock.Get(idGenerator)
                .Setup(g => g.GetNextIdAsync(It.IsAny<string>()))
                .ReturnsAsync(() => idToReturn);
            return idGenerator;
        }

        public IUrlValidator GetAlwaysGoodUrlValidator()
        {
            var validator = Mock.Of<IUrlValidator>();
            Mock.Get(validator)
                .Setup(g => g.IsValid(It.IsAny<string>()))
                .Returns(true);
            return validator;
        }
    }
}
