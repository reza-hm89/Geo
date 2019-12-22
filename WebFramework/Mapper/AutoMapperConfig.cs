using AutoMapper;
using DataContract.Geographical;
using DataContract.User;
using Model.Geographical;
using Model.Users;

namespace WebFramework.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<UserDistance, UserDistanceDto>();
            CreateMap<UserDistanceDto, UserDistance>();
        }
    }
}
