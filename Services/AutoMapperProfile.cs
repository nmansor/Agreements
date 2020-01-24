using AutoMapper;
using DAL.Entities;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
       

            CreateMap<Notarizer, NotarizerDTO>();
            CreateMap<NotarizerDTO, Notarizer>();
            CreateMap<User, UserDTO>().ForMember(s => s.Password, d => d.Ignore());
            CreateMap<UserDTO, User>().ForMember(s => s.PasswordHash, d => d.Ignore()).ForMember(s => s.Id, d => d.Ignore());
            
        }
    }
}
