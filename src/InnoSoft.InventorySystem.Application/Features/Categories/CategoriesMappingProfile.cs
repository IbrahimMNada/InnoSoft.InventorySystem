using AutoMapper;
using InnoSoft.InventorySystem.Application.AutoMapperReslovers;
using InnoSoft.InventorySystem.Application.Features.Categories.Commands;
using InnoSoft.InventorySystem.Application.Features.Categories.Dtos;
using InnoSoft.InventorySystem.Core.Entities.Categories;


namespace InnoSoft.InventorySystem.Application.Features.Categories
{
    public class CategoriesMappingProfile : Profile
    {
        public CategoriesMappingProfile()
        {
            _ = CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            _ = CreateMap<UpdateCategoryCommand, Category>().ReverseMap();
            _ = CreateMap<Category, CategoryAdministrationDto>();

            _ = CreateMap<CategoryTranslation, CategoryTranslationDto>()
                .ForMember(x => x.Language, otp => otp.MapFrom<TranslationDtoReslover>());


            _ = CreateMap<CategoryTranslationDto, CategoryTranslation>()
                            .ForMember(x => x.LanguageId, otp => otp.MapFrom<TranslationEntityReslover>());



            _ = CreateMap<Category, CategoryDto>()
                   .ForMember(x => x.Name, otp =>
                   otp.MapFrom<CurrentTranslationResolver<CategoryTranslation, string>, string>(p => nameof(CategoryTranslation.Name)))
                   .ForMember(x => x.Description, otp =>
                   otp.MapFrom<CurrentTranslationResolver<CategoryTranslation, string>, string>(p => nameof(CategoryTranslation.Description)));


        }
    }
}
