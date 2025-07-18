using AutoMapper;
using InnoSoft.InventorySystem.Application.Localization;
using InnoSoft.InventorySystem.Core;
using InnoSoft.InventorySystem.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.AutoMapperReslovers
{
    [DependencyScannerIgnore]
    public class TranslationDtoReslover : IValueResolver<ITranslation, IWrriteTranslationDto, string>
    {
        private readonly IServiceProvider _serviceProvider;
        public TranslationDtoReslover(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public string Resolve(ITranslation source, IWrriteTranslationDto destination, string destMember, ResolutionContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _languageService = scope.ServiceProvider.GetRequiredService<ILanguageService>();
                var lang = _languageService.GetAll().Where(x => x.Id == source.LanguageId).FirstOrDefault();
                return lang.Abbreviation;
            }
        }
    }
}
