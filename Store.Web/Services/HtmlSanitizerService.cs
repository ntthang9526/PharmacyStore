using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace PharmacyWeb.Services;

public interface IHtmlSanitizerService
{
    string Sanitize(string? html);
}

public class HtmlSanitizerService : IHtmlSanitizerService
{
    // Allowed HTML tags for formatted medical product descriptions
    private static readonly HashSet<string> AllowedTags = new(StringComparer.OrdinalIgnoreCase)
    {
        "p", "br", "b", "strong", "i", "em", "u", "s", "strike",
        "ul", "ol", "li", "h1", "h2", "h3", "h4", "h5", "h6",
        "blockquote", "span", "div", "table", "thead", "tbody", "tr", "th", "td",
        "a", "img", "hr"
    };

    // Void / Self-closing tags
    private static readonly HashSet<string> VoidTags = new(StringComparer.OrdinalIgnoreCase)
    {
        "br", "hr", "img"
    };

    // Dangerous container tags whose inner content must also be stripped
    private static readonly HashSet<string> DangerousContainerTags = new(StringComparer.OrdinalIgnoreCase)
    {
        "script", "style", "iframe", "object", "embed", "applet", "form",
        "svg", "math", "canvas", "template", "noscript", "meta", "link",
        "base", "frame", "frameset", "video", "audio"
    };

    // Allowed attributes per tag (lowercase keys and values)
    private static readonly Dictionary<string, HashSet<string>> AllowedAttributes = new(StringComparer.OrdinalIgnoreCase)
    {
        ["a"] = new(StringComparer.OrdinalIgnoreCase) { "href", "title", "target", "rel", "class" },
        ["img"] = new(StringComparer.OrdinalIgnoreCase) { "src", "alt", "title", "width", "height", "class" },
        ["th"] = new(StringComparer.OrdinalIgnoreCase) { "colspan", "rowspan", "class" },
        ["td"] = new(StringComparer.OrdinalIgnoreCase) { "colspan", "rowspan", "class" },
        ["ol"] = new(StringComparer.OrdinalIgnoreCase) { "start", "type", "class" },
        ["span"] = new(StringComparer.OrdinalIgnoreCase) { "class", "title" },
        ["div"] = new(StringComparer.OrdinalIgnoreCase) { "class", "title" },
        ["p"] = new(StringComparer.OrdinalIgnoreCase) { "class", "title" },
        ["table"] = new(StringComparer.OrdinalIgnoreCase) { "class" }
    };

    private static readonly HashSet<string> CommonAllowedAttrs = new(StringComparer.OrdinalIgnoreCase)
    {
        "class", "title"
    };

    public string Sanitize(string? html)
    {
        if (string.IsNullOrWhiteSpace(html))
            return string.Empty;

        var sb = new StringBuilder(html.Length);
        int i = 0;
        int len = html.Length;

        while (i < len)
        {
            if (html[i] == '<')
            {
                // 1. Comments: <!-- ... -->
                if (i + 3 < len && html[i + 1] == '!' && html[i + 2] == '-' && html[i + 3] == '-')
                {
                    int endComment = html.IndexOf("-->", i + 4, StringComparison.Ordinal);
                    i = endComment == -1 ? len : endComment + 3;
                    continue;
                }

                // 2. CDATA: <![CDATA[ ... ]]>
                if (i + 8 < len && html.Substring(i, 9).Equals("<![CDATA[", StringComparison.OrdinalIgnoreCase))
                {
                    int endCdata = html.IndexOf("]]>", i + 9, StringComparison.Ordinal);
                    i = endCdata == -1 ? len : endCdata + 3;
                    continue;
                }

                // 3. DOCTYPE or other <! declarations
                if (i + 1 < len && html[i + 1] == '!')
                {
                    int endDecl = FindTagEnd(html, i + 2);
                    i = endDecl == -1 ? len : endDecl + 1;
                    continue;
                }

                // 4. Parse normal HTML tag
                int tagEnd = FindTagEnd(html, i + 1);
                if (tagEnd == -1)
                {
                    // Unclosed '<' tag at the end, break
                    break;
                }

                string tagContent = html.Substring(i + 1, tagEnd - (i + 1)).Trim();
                i = tagEnd + 1;

                if (string.IsNullOrWhiteSpace(tagContent))
                    continue;

                bool isClosing = tagContent.StartsWith('/');
                if (isClosing)
                {
                    tagContent = tagContent.Substring(1).TrimStart();
                }

                // Extract tag name (letters, numbers)
                int nameEnd = 0;
                while (nameEnd < tagContent.Length &&
                       (char.IsLetterOrDigit(tagContent[nameEnd]) || tagContent[nameEnd] == '-' || tagContent[nameEnd] == ':'))
                {
                    nameEnd++;
                }

                if (nameEnd == 0)
                    continue;

                string tagName = tagContent.Substring(0, nameEnd).ToLowerInvariant();
                string remainder = tagContent.Substring(nameEnd);

                // If dangerous container tag, skip all content until closing tag </tagName>
                if (DangerousContainerTags.Contains(tagName))
                {
                    if (!isClosing && !remainder.TrimEnd().EndsWith('/'))
                    {
                        // Look for closing </tagName>
                        var closePattern = new Regex($@"</\s*{Regex.Escape(tagName)}\s*>", RegexOptions.IgnoreCase);
                        var match = closePattern.Match(html, i);
                        if (match.Success)
                        {
                            i = match.Index + match.Length;
                        }
                        // If no closing tag was found, just discard this opening tag (i is already at tagEnd + 1)
                    }
                    continue;
                }

                // If not in allowed tags whitelist, discard tag
                if (!AllowedTags.Contains(tagName))
                {
                    continue;
                }

                if (isClosing)
                {
                    if (!VoidTags.Contains(tagName))
                    {
                        sb.Append("</").Append(tagName).Append('>');
                    }
                    continue;
                }

                // Parse attributes for opening tag
                bool isSelfClosing = VoidTags.Contains(tagName) || remainder.TrimEnd().EndsWith('/');
                string sanitizedAttrs = ParseAndSanitizeAttributes(tagName, remainder);

                sb.Append('<').Append(tagName);
                if (!string.IsNullOrEmpty(sanitizedAttrs))
                {
                    sb.Append(' ').Append(sanitizedAttrs);
                }

                if (VoidTags.Contains(tagName))
                {
                    sb.Append(" />");
                }
                else
                {
                    sb.Append('>');
                }
            }
            else
            {
                sb.Append(html[i]);
                i++;
            }
        }

        return sb.ToString().Trim();
    }

    private static int FindTagEnd(string html, int start)
    {
        bool inSingleQuote = false;
        bool inDoubleQuote = false;
        int len = html.Length;

        for (int i = start; i < len; i++)
        {
            char c = html[i];
            if (c == '"' && !inSingleQuote)
            {
                inDoubleQuote = !inDoubleQuote;
            }
            else if (c == '\'' && !inDoubleQuote)
            {
                inSingleQuote = !inSingleQuote;
            }
            else if (c == '>' && !inSingleQuote && !inDoubleQuote)
            {
                return i;
            }
        }

        return -1;
    }

    private static string ParseAndSanitizeAttributes(string tagName, string rawAttributes)
    {
        if (string.IsNullOrWhiteSpace(rawAttributes))
            return string.Empty;

        var allowedSet = AllowedAttributes.TryGetValue(tagName, out var set) ? set : CommonAllowedAttrs;
        var validPairs = new List<string>();

        int pos = 0;
        int len = rawAttributes.Length;

        while (pos < len)
        {
            // Skip whitespace, slashes, or commas
            while (pos < len && (char.IsWhiteSpace(rawAttributes[pos]) || rawAttributes[pos] == '/'))
            {
                pos++;
            }

            if (pos >= len) break;

            // Read attribute name
            int nameStart = pos;
            while (pos < len && !char.IsWhiteSpace(rawAttributes[pos]) && rawAttributes[pos] != '=' && rawAttributes[pos] != '/')
            {
                pos++;
            }

            if (pos == nameStart)
            {
                pos++;
                continue;
            }

            string attrName = rawAttributes.Substring(nameStart, pos - nameStart).ToLowerInvariant();

            // Skip whitespace before '='
            while (pos < len && char.IsWhiteSpace(rawAttributes[pos]))
            {
                pos++;
            }

            string? attrValue = null;
            if (pos < len && rawAttributes[pos] == '=')
            {
                pos++; // Skip '='
                // Skip whitespace after '='
                while (pos < len && char.IsWhiteSpace(rawAttributes[pos]))
                {
                    pos++;
                }

                if (pos < len)
                {
                    char quote = rawAttributes[pos];
                    if (quote == '"' || quote == '\'')
                    {
                        pos++; // Skip opening quote
                        int valStart = pos;
                        while (pos < len && rawAttributes[pos] != quote)
                        {
                            pos++;
                        }
                        attrValue = rawAttributes.Substring(valStart, pos - valStart);
                        if (pos < len && rawAttributes[pos] == quote)
                        {
                            pos++; // Skip closing quote
                        }
                    }
                    else
                    {
                        // Unquoted value: ends at whitespace or '/' or '>'
                        int valStart = pos;
                        while (pos < len && !char.IsWhiteSpace(rawAttributes[pos]) && rawAttributes[pos] != '/' && rawAttributes[pos] != '>')
                        {
                            pos++;
                        }
                        attrValue = rawAttributes.Substring(valStart, pos - valStart);
                    }
                }
            }

            // Reject any event handler attribute (e.g. onload, onclick, onerror, onmouseover...)
            if (attrName.StartsWith("on", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            // Check if attribute is in allowed whitelist for this tag
            if (!allowedSet.Contains(attrName))
            {
                continue;
            }

            attrValue ??= string.Empty;

            // Sanitize URI attributes (href, src)
            if (attrName == "href" || attrName == "src")
            {
                var sanitizedUri = SanitizeUri(attrValue, attrName == "src");
                validPairs.Add($"{attrName}=\"{WebUtility.HtmlEncode(sanitizedUri)}\"");
            }
            else if (attrName == "target")
            {
                // Only allow _blank, _self
                if (attrValue.Equals("_blank", StringComparison.OrdinalIgnoreCase))
                {
                    validPairs.Add("target=\"_blank\" rel=\"noopener noreferrer\"");
                }
                else
                {
                    validPairs.Add("target=\"_self\"");
                }
            }
            else
            {
                validPairs.Add($"{attrName}=\"{WebUtility.HtmlEncode(attrValue)}\"");
            }
        }

        return string.Join(" ", validPairs);
    }

    private static string SanitizeUri(string uri, bool isSrc)
    {
        if (string.IsNullOrWhiteSpace(uri))
            return isSrc ? string.Empty : "#";

        // Decode HTML entities
        var decoded = WebUtility.HtmlDecode(uri).Trim();

        // Strip embedded whitespace and control characters from scheme check
        var normalized = Regex.Replace(decoded, @"[\s\0\r\n\t]+", string.Empty).ToLowerInvariant();

        if (normalized.StartsWith("javascript:") ||
            normalized.StartsWith("vbscript:") ||
            normalized.StartsWith("data:text/html") ||
            normalized.StartsWith("data:application") ||
            (!normalized.StartsWith("data:image/") && normalized.StartsWith("data:")))
        {
            return isSrc ? string.Empty : "#";
        }

        // Relative URIs or safe schemes
        if (decoded.StartsWith('/') ||
            decoded.StartsWith('#') ||
            decoded.StartsWith("./") ||
            normalized.StartsWith("http://") ||
            normalized.StartsWith("https://") ||
            (!isSrc && normalized.StartsWith("mailto:")))
        {
            return decoded;
        }

        // Allow data:image/... for src
        if (isSrc && normalized.StartsWith("data:image/"))
        {
            return decoded;
        }

        return isSrc ? string.Empty : "#";
    }
}
