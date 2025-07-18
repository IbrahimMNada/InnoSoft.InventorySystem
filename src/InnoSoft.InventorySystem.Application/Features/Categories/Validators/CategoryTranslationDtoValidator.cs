using FluentValidation;
using InnoSoft.InventorySystem.Application.Features.Categories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Features.Categories.Validators
{
    public class CategoryTranslationDtoValidator : AbstractValidator<CategoryTranslationDto>
    {
        public CategoryTranslationDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Language).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().NotEmpty();
        }
    }
}
