using Microsoft.AspNetCore.Http;

namespace InnoSoft.InventorySystem.Application.Localization
{
    public class LocalizationLanguageAccessor : ILocalizationLanguageAccessor
    {
        private const string LanguageCodeHeaderName = "x-Accept-Language";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocalizationLanguageAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetLanguageCode()
        {
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null || _httpContextAccessor?.HttpContext?.Request?.Headers?.ContainsKey(LanguageCodeHeaderName) == false)
                return "ar";
            return _httpContextAccessor.HttpContext.Request.Headers[LanguageCodeHeaderName].ToString().Contains("ar") ? "ar" : "en";
        }
        public string GetOppositeCode()
        {
            return GetLanguageCode() == "ar" ? "en" : "ar";
        }
    }
}
