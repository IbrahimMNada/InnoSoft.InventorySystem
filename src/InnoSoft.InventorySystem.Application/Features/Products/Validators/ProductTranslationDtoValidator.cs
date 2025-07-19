using FluentValidation;
using InnoSoft.InventorySystem.Application.Features.Products.Dtos;
using InnoSoft.InventorySystem.Localization;
using Microsoft.Extensions.Localization;

namespace InnoSoft.InventorySystem.Application.Features.Products.Validators
{
    public class ProductTranslationDtoValidator : AbstractValidator<ProductTranslationDto>
    {
        public ProductTranslationDtoValidator(IStringLocalizer<SharedResource> localizer)
        {
            _ = RuleFor(x => x.Name).NotEmpty();
            _ = RuleFor(x => x.Language).NotEmpty();
            _ = RuleFor(x => x.Description).MaximumLength(70).WithMessage(localizer["MaximumLength"]);
        }
    }
}
