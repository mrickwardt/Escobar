using AutoMapper;
using Server.DTOs;
using Server.Models;

namespace Server.AutoMapper
{
    public class UserMapper: Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserOutputDto>().ReverseMap();
        }
    }
}