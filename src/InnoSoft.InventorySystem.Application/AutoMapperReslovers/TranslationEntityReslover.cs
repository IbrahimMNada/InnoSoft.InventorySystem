using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.AutoMapperReslovers
{
    [DependencyScannerIgnore]
    public class TranslationEntityReslover : IValueResolver<IWrriteTranslationDto, ITranslation, int>
    {
        private readonly IServiceProvider _serviceProvider;
        public TranslationEntityReslover(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public int Resolve(IWrriteTranslationDto source, ITranslation destination, int destMember, ResolutionContext context)
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
