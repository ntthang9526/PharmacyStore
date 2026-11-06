using System.Text.RegularExpressions;

namespace PharmacyWeb.Services;

public interface IHtmlSanitizerService
{
    string Sanitize(string? html);
}

public class HtmlSanitizerService : IHtmlSanitizerService
{
    private static readonly Regex ScriptRegex = new(@"<script[^>]*>[\s\S]*?</script>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex StyleRegex = new(@"<style[^>]*>[\s\S]*?</style>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex IframeRegex = new(@"<iframe[^>]*>[\s\S]*?</iframe>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex EventAttributes = new(@"\s+(on[a-z]+)\s*=\s*(""[^""]*""|'[^']*'|[^\s>]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex JavascriptHref = new(@"href\s*=\s*[""']\s*javascript:[^""']*[""']", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public string Sanitize(string? html)
    {
        if (string.IsNullOrWhiteSpace(html))
            return string.Empty;

        var clean = ScriptRegex.Replace(html, string.Empty);
        clean = StyleRegex.Replace(clean, string.Empty);
        clean = IframeRegex.Replace(clean, string.Empty);
        clean = EventAttributes.Replace(clean, string.Empty);
        clean = JavascriptHref.Replace(clean, "href=\"#\"");

        return clean.Trim();
    }
}
