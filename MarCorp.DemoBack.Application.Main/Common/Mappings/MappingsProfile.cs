﻿using AutoMapper;
using MarCorp.DemoBack.Application.DTO;
using MarCorp.DemoBack.Domain.Models.Entities;
using MarCorp.DemoBack.Domain.Models.Events;

namespace MarCorp.DemoBack.Application.UseCases.Common.Mappings
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Discount, DiscountDTO>().ReverseMap();
            CreateMap<Discount, DiscountCreatedEvent>().ReverseMap();
        }
    }
}