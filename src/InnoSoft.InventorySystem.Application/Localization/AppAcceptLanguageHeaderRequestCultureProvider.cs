using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Localization
{
    public class AppAcceptLanguageHeaderRequestCultureProvider : RequestCultureProvider
    {
        public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.TryGetValue("x-Accept-Language", out StringValues languages) || StringValues.IsNullOrEmpty(languages))
            {
                return Task.FromResult<ProviderCultureResult?>(null);
            }

            var acceptedLanguages = languages.ToString()
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            // Example: you can prioritize the first supported language
            var supportedCultures = new[] { "en", "ar" };

            foreach (var lang in acceptedLanguages)
            {
                var normalizedLang = lang.Split(';')[0]; // remove quality values (e.g., en-US;q=0.9)
                if (supportedCultures.Contains(normalizedLang, StringComparer.OrdinalIgnoreCase))
                {
                    return Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(normalizedLang, normalizedLang));
                }
            }

            return Task.FromResult<ProviderCultureResult?>(null);
        }
    }
}