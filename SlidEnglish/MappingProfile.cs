using AutoMapper;
using SlidFinance.App;
using SlidEnglish.Domain;
using SlidEnglish.Infrastructure;
using System.Linq;

namespace SlidEnglish.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile(ApplicationDbContext context)
        {
			CreateMap<WordViewModel, Word>()
				.ForMember(dest => dest.Association,
					opt => opt.MapFrom(src => src.Association ?? ""))
				.ForMember(dest => dest.Description,
					opt => opt.MapFrom(src => src.Description ?? ""));

			CreateMap<Word, WordViewModel>();
		}
    }
}
