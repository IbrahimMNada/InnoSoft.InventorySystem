using InnoSoft.InventorySystem.Application.Localization;
using InnoSoft.InventorySystem.Core;
using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultationPlatformService.Application.Localization
{
    [DependencyScannerIgnore]
    public class LanguageService : ILanguageService
    {
        private readonly IRepository<Language> _languageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LanguageService(IRepository<Language> languageRepository, IHttpContextAccessor httpContextAccessor)
        {
            _languageRepository = languageRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Language> GetAll()
        {
            return _languageRepository.GetAll().ToList();
        }

        public Language GetCurrentLanguage()
        {
            var requestLanguage = _httpContextAccessor.HttpContext.Request.Headers["accept-language"].FirstOrDefault();
            return _languageRepository.GetAll().ToList().FirstOrDefault(x => x.Abbreviation == requestLanguage) ??
                   _languageRepository.GetAll().ToList().FirstOrDefault(x => x.Abbreviation == "ar");
        }
    }
}
