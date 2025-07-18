using FluentValidation;
using InnoSoft.InventorySystem.Application.Features.Categories.Commands;
using InnoSoft.InventorySystem.Localization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Features.Categories.Validators
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage(localizer["Required"]);
            RuleForEach(x => x.Translations).SetValidator(new CategoryTranslationDtoValidator(localizer));
        }
    }
}

