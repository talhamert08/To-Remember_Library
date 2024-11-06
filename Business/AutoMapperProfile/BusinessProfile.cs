using AutoMapper;
using Entity.Concrete;

namespace Business.AutoMapperProfile
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            //CreateMap<GuidanceQuestion, GuidanceQuestionDto>().ReverseMap();
            CreateMap<Book,BookDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();

        }
    }
}
