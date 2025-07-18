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
    public class TranslationEntityReslover : IValueResolver<IWrriteTranslationDto, ITranslation, Guid>
    {
        private readonly IServiceProvider _serviceProvider;
        public TranslationEntityReslover(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Guid Resolve(IWrriteTranslationDto source, ITranslation destination, Guid destMember, ResolutionContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _languageService = scope.ServiceProvider.GetRequiredService<ILanguageService>();
                var lang = _languageService.GetAll().Where(x => x.Abbreviation == source.Language).FirstOrDefault();
                return lang.Id;
            }
        }

    }
}
