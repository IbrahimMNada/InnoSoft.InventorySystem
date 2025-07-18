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
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<UpdateCategoryCommand,Category>().ReverseMap();
            CreateMap<Category, CategoryAdministrationDto>();

            CreateMap<CategoryTranslation, CategoryTranslationDto>()
                .ForMember(x => x.Language, otp => otp.MapFrom<TranslationDtoReslover>());


            CreateMap<CategoryTranslationDto, CategoryTranslation>()
                            .ForMember(x => x.LanguageId, otp => otp.MapFrom<TranslationEntityReslover>());



            CreateMap<Category, CategoryDto>()
                   .ForMember(x => x.Name, otp =>
                   otp.MapFrom<CurrentTranslationResolver<CategoryTranslation, string>, string>(p => nameof(CategoryTranslation.Name)))
                   .ForMember(x => x.Description, otp =>
                   otp.MapFrom<CurrentTranslationResolver<CategoryTranslation, string>, string>(p => nameof(CategoryTranslation.Description)));


        }
    }
}
