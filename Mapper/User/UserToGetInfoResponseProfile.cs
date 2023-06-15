using AutoMapper;
using BusinessObject.API.Response;

namespace Mapper.User
{
    public class UserToGetInfoResponseProfile : Profile
    {
        public UserToGetInfoResponseProfile() 
        {
            CreateMap<BusinessObject.Models.User, GetInfoResponseModel>();
        }
    }
}
