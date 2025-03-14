﻿using AutoMapper;
using MarCorp.DemoBack.Application.DTO;
using MarCorp.DemoBack.Domain.Models.Entities;

namespace MarCorp.DemoBack.Support.Mapper
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
