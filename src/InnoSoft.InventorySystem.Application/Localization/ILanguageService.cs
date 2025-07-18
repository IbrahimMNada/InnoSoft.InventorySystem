using InnoSoft.InventorySystem.Core.Entities;

namespace InnoSoft.InventorySystem.Application.Localization
{
    public interface ILanguageService
    {
        List<Language> GetAll();
        Language GetCurrentLanguage();
    }
}