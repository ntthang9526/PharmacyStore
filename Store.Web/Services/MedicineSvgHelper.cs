using System.Web;

namespace PharmacyWeb.Services;

public static class MedicineSvgHelper
{
    public static string GeneratePackagingSvg(string name, string? activeIngredient, string? packagingSpec, string? unit, bool isPrescription)
    {
        var accent = isPrescription ? "#0284C7" : "#0F766E";
        var softBg = isPrescription ? "#E0F2FE" : "#CCFBF1";
        var lightTint = isPrescription ? "#F0F9FF" : "#F0FDFA";

        var shortName = (string.IsNullOrWhiteSpace(name) ? "Pharmacy" : name);
        if (shortName.Length > 22) shortName = shortName[..22];

        var subText = (string.IsNullOrWhiteSpace(activeIngredient) ? "Chăm sóc sức khỏe" : activeIngredient);
        if (subText.Length > 32) subText = subText[..32];

        var specText = (string.IsNullOrWhiteSpace(packagingSpec) ? (string.IsNullOrWhiteSpace(unit) ? "Hộp" : unit) : packagingSpec);
        if (specText.Length > 28) specText = specText[..28];

        var escapedName = HttpUtility.HtmlEncode(shortName);
        var escapedSub = HttpUtility.HtmlEncode(subText);
        var escapedSpec = HttpUtility.HtmlEncode(specText);

        var svg = $@"<svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 320 220"" width=""320"" height=""220"">
  <rect width=""320"" height=""220"" rx=""12"" fill=""#FFFFFF""/>
  <rect x=""34"" y=""20"" width=""252"" height=""180"" rx=""14"" fill=""#FFFFFF"" stroke=""#E2E8F0"" stroke-width=""2""/>
  <path d=""M34 34 C34 26 40 20 48 20 L272 20 C280 20 286 26 286 34 L286 68 L34 68 Z"" fill=""{lightTint}""/>
  <rect x=""34"" y=""64"" width=""252"" height=""4"" fill=""{accent}""/>
  <circle cx=""62"" cy=""44"" r=""14"" fill=""{softBg}""/>
  <path d=""M62 36 v16 M54 44 h16"" stroke=""{accent}"" stroke-width=""3"" stroke-linecap=""round""/>
  <text x=""86"" y=""48"" fill=""{accent}"" font-family=""'Be Vietnam Pro', sans-serif"" font-size=""11.5"" font-weight=""800"" letter-spacing=""0.6"">PHARMACY</text>
  <text x=""50"" y=""106"" fill=""#0F172A"" font-family=""'Be Vietnam Pro', sans-serif"" font-size=""17"" font-weight=""800"">{escapedName}</text>
  <text x=""50"" y=""130"" fill=""#475569"" font-family=""'Be Vietnam Pro', sans-serif"" font-size=""12"" font-weight=""600"">{escapedSub}</text>
  <rect x=""50"" y=""152"" width=""160"" height=""26"" rx=""13"" fill=""#F8FAFC"" stroke=""#E2E8F0""/>
  <text x=""64"" y=""169"" fill=""#334155"" font-family=""'Be Vietnam Pro', sans-serif"" font-size=""11"" font-weight=""600"">{escapedSpec}</text>
</svg>";

        return "data:image/svg+xml;utf8," + Uri.EscapeDataString(svg);
    }
}
