using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using InnoSoft.InventorySystem.Core;
using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Application.Localization;

namespace InnoSoft.InventorySystem.Application.AutoMapperReslovers
{
    [DependencyScannerIgnore]
    public class CurrentTranslationResolver<T, DistType> : IMemberValueResolver<ITranslationRootOf<T>, object, string, DistType>
        where T : ITranslation
        where DistType : class
    {
        private readonly ILanguageService _languageService;
        public CurrentTranslationResolver(ILanguageService languageService)
        {
            _languageService = languageService;
        }


        public DistType Resolve(ITranslationRootOf<T> source, object destination, string sourceMember, DistType destMember, ResolutionContext context)
        {
            var currentLanguage = _languageService.GetCurrentLanguage();
            var values = source.Translations.Where(x => x.LanguageId == currentLanguage.Id).FirstOrDefault() !=
                null ? source.Translations.Where(x => x.LanguageId == currentLanguage.Id).FirstOrDefault() : source.Translations.FirstOrDefault();
            if (values != null)
            {
                var value = values.GetType().GetProperty(sourceMember).GetValue(values, null);
                if (value == null || value == "")
                {
                    var fallbackTranslations = source.Translations.Where(x => x.LanguageId != currentLanguage.Id).FirstOrDefault();
                    if (fallbackTranslations != null)
                    {
                        var fallBackValue = fallbackTranslations.GetType().GetProperty(sourceMember).GetValue(fallbackTranslations, null);
                        return (DistType)Convert.ChangeType(fallBackValue, typeof(DistType));
                    }
                }
                return (DistType)Convert.ChangeType(value, typeof(DistType));
            }
            return null;
        }
    }
}
