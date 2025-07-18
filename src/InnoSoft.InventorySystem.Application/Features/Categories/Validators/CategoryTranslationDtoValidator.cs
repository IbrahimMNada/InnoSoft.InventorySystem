using FluentValidation;
using InnoSoft.InventorySystem.Application.Features.Categories.Dtos;
using InnoSoft.InventorySystem.Localization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Features.Categories.Validators
{
    public class CategoryTranslationDtoValidator : AbstractValidator<CategoryTranslationDto>
    {
        public CategoryTranslationDtoValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage(localizer["Required"]);
            RuleFor(x => x.Language).NotNull().NotEmpty().WithMessage(localizer["Required"]);
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage(localizer["Required"]);

        }
    }
}
