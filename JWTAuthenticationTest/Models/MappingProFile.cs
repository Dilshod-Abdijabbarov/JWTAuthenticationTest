using AutoMapper;

namespace JWTAuthenticationTest.Models
{
    public class MappingProFile:Profile
    {
        public MappingProFile() 
        {
            CreateMap<UserModel, UserModelDTO>().ReverseMap();
        }
    }
}
