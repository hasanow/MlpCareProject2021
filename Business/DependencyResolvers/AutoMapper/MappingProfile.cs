using AutoMapper;
using BaseProject.Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User_T, UserDto>();
            CreateMap<UserForRegisterDto, UserForLoginDto>();
            CreateMap<UserDto, User_T>();
            CreateMap<UserListModel, User_T>();
            CreateMap<User_T,UserListModel>();
        }
    }
}
