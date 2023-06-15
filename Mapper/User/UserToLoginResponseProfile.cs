using AutoMapper;
using BusinessObject.API.Response;

namespace Mapper.User
{
    public class UserToLoginResponseProfile : Profile
    {
        public UserToLoginResponseProfile() 
        {
            CreateMap<BusinessObject.Models.User, LoginResponseModel>()
                .ForMember(des => des.Role, opt => opt.Ignore())
                .ForMember(des => des.Token, opt => opt.Ignore());
        }
    }
}
