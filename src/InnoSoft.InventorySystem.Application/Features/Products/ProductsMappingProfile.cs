using AutoMapper;
using InnoSoft.InventorySystem.Application.AutoMapperReslovers;
using InnoSoft.InventorySystem.Application.Features.Products.Commands;
using InnoSoft.InventorySystem.Application.Features.Products.Dtos;
using InnoSoft.InventorySystem.Core.Entities.Categories;
using InnoSoft.InventorySystem.Core.Entities.Products;

namespace InnoSoft.InventorySystem.Application.Features.Products
{
    public class ProductsMappingProfile : Profile
    {
        public ProductsMappingProfile()
        {
            _ = CreateMap<Product, CreateProductCommand>().ReverseMap();
            _ = CreateMap<Product, UpdateProductCommand>().ReverseMap();

            _ = CreateMap<Product, ProductAdministrationDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category ?? new Category() { Translations = new List<CategoryTranslation>() }));

            _ = CreateMap<ProductAdministrationDto, Product>();

            _ = CreateMap<ProductNotificationDto, Product>();
            _ = CreateMap<Product, ProductNotificationDto>();

            _ = CreateMap<ProductTranslation, ProductTranslationDto>()
                .ForMember(x => x.Language, otp => otp.MapFrom<TranslationDtoReslover>());

            _ = CreateMap<ProductTranslationDto, ProductTranslation>()
                 .ForMember(x => x.LanguageId, otp => otp.MapFrom<TranslationEntityReslover>());

            _ = CreateMap<Product, ProductDto>()
                   .ForMember(x => x.Name, otp =>
                   otp.MapFrom<CurrentTranslationResolver<ProductTranslation, string>, string>(p => nameof(ProductTranslation.Name)))
                   .ForMember(x => x.Description, otp =>
                   otp.MapFrom<CurrentTranslationResolver<ProductTranslation, string>, string>(p => nameof(ProductTranslation.Description)));
        }
    }
}
