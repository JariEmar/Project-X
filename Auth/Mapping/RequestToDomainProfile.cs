using AutoMapper;
using Domain.Entities;
using Api.Contracts.v1.Requests;
using Api.Contracts.v1.Responses;
using Domain.Common;
using Api.Contracts.v1.Requests.Queries;

namespace Api.Mapping
{
    public class RequestToDomainProfile : Profile
    {

        public RequestToDomainProfile()
        {
            CreateMap<PostRequest, Post>();
            CreateMap<CreatePostRequest, Post>();
            CreateMap<UpdatePostRequest, Post>();
            CreateMap<TagRequest, Tag>();

            CreateMap<PaginationQuery, PaginationFilter>();
        }
    }   
}
