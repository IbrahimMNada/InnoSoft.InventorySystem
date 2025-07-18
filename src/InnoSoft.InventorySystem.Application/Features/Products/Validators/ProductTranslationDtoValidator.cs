using FluentValidation;
using InnoSoft.InventorySystem.Application.Features.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Features.Products.Validators
{
    public class ProductTranslationDtoValidator : AbstractValidator<ProductTranslationDto>
    {
        public ProductTranslationDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Language).NotEmpty();
        }
    }
}
