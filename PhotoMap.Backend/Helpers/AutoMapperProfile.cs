using AutoMapper;
using PhotoMap.Backend.Entities;
using PhotoMap.Backend.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMap.Backend.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserResponseModel>();
            CreateMap<UserRegisterModel, User>();
            CreateMap<UserUpdateModel, User>();
        }
    }
}
