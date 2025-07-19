using FluentValidation;
using InnoSoft.InventorySystem.Application.Features.Categories.Dtos;
using InnoSoft.InventorySystem.Localization;
using Microsoft.Extensions.Localization;

namespace InnoSoft.InventorySystem.Application.Features.Categories.Validators
{
    public class CategoryTranslationDtoValidator : AbstractValidator<CategoryTranslationDto>
    {
        public CategoryTranslationDtoValidator(IStringLocalizer<SharedResource> localizer)
        {
            _ = RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage(localizer["Required"]);
            _ = RuleFor(x => x.Language).NotNull().NotEmpty().WithMessage(localizer["Required"]);
            _ = RuleFor(x => x.Description).MaximumLength(70).WithMessage(localizer["MaximumLength"]);
        }
    }
}
