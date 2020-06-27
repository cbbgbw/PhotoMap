using AutoMapper;
using PhotoMap.Backend.Entities;
using PhotoMap.Backend.Models.Photos;
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
            //User
            CreateMap<User, UserResponseModel>();
            CreateMap<UserRegisterModel, User>();
            CreateMap<UserUpdateModel, User>();

            //Photo
            CreateMap<Photo, PhotoResponseModel>();
            CreateMap<PhotoInsertModel, Photo>();
            CreateMap<PhotoUpdateModel, Photo>();
        }
    }
}
