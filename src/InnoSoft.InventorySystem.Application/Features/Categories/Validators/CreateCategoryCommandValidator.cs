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
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator(IStringLocalizer<SharedResource> localizer)
        {
            //RuleFor(x => x.Icon).NotNull().NotEmpty();
            RuleForEach(x => x.Translations).SetValidator(new CategoryTranslationDtoValidator(localizer));
        }
    }
}

