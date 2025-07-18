using FluentValidation;
using InnoSoft.InventorySystem.Application.Features.Products.Commands;

namespace InnoSoft.InventorySystem.Application.Features.Products.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Translations).NotEmpty();
            RuleForEach(x => x.Translations).SetValidator(new ProductTranslationDtoValidator());
            RuleFor(x => x.PricePerPiece).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
            RuleFor(x => x.AlertThresholdQuantity).GreaterThanOrEqualTo(0);
            RuleFor(x => x.CategoryId).NotEmpty();
        }
    }
}
