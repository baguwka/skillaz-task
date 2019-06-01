namespace Shortener.Lib.Shorten
{
    public interface IUrlValidator
    {
        bool IsValid(string url);
    }
}