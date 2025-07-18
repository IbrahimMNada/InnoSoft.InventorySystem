using AutoMapper;
using InnoSoft.InventorySystem.Application.AutoMapperReslovers;
using InnoSoft.InventorySystem.Application.Features.Categories.Commands;
using InnoSoft.InventorySystem.Application.Features.Categories.Dtos;
using InnoSoft.InventorySystem.Application.Features.Products.Commands;
using InnoSoft.InventorySystem.Application.Features.Products.Dtos;
using InnoSoft.InventorySystem.Core.Entities.Categories;
using InnoSoft.InventorySystem.Core.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Features.Products
{
    public class ProductsMappingProfile : Profile
    {
        public ProductsMappingProfile()
        {
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            CreateMap<Product, UpdateProductCommand>().ReverseMap();

            CreateMap<ProductTranslation, ProductTranslationDto>()
                .ForMember(x => x.Language, otp => otp.MapFrom<TranslationDtoReslover>());

            CreateMap<ProductTranslationDto, ProductTranslation>()
                 .ForMember(x => x.LanguageId, otp => otp.MapFrom<TranslationEntityReslover>());

            CreateMap<Product, ProductDto>()
                   .ForMember(x => x.Name, otp =>
                   otp.MapFrom<CurrentTranslationResolver<ProductTranslation, string>, string>(p => nameof(ProductTranslation.Name)))
                   .ForMember(x => x.Description, otp =>
                   otp.MapFrom<CurrentTranslationResolver<ProductTranslation, string>, string>(p => nameof(ProductTranslation.Description)));
        }
    }
}
