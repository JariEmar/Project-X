using AutoMapper;
using Domain.Entities;
using Api.Contracts.v1.Requests;
using Api.Contracts.v1.Responses;

namespace Api.Mapping
{
    public class DomainToResponseProfile : Profile
    {

        public DomainToResponseProfile()
        {
            CreateMap<Post, PostResponse>();
            CreateMap<Tag, TagResponse>();
        }
    }   
}
