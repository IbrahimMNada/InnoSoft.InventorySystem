using FluentValidation;
using InnoSoft.InventorySystem.Application.Features.Products.Commands;
using InnoSoft.InventorySystem.Localization;
using Microsoft.Extensions.Localization;

namespace InnoSoft.InventorySystem.Application.Features.Products.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator(IStringLocalizer<SharedResource> localizer)
        {
            _ = RuleFor(x => x.Id).NotEmpty();
            _ = RuleFor(x => x.Translations).NotEmpty();
            _ = RuleForEach(x => x.Translations).SetValidator(new ProductTranslationDtoValidator(localizer));
            _ = RuleFor(x => x.PricePerPiece).GreaterThanOrEqualTo(0);
            _ = RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
            _ = RuleFor(x => x.AlertThresholdQuantity).GreaterThanOrEqualTo(0);
            _ = RuleFor(x => x.CategoryId).NotEmpty();
        }
    }
}
