using AutoMapper;
using BlogManagementDomain.Blog;
using BlogManagementDomain.Domain;
using BlogManagementDomain.Dto.Request;
using BlogManagementDomain.Dto.Response;

namespace BlogManagementDomain.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BlogPostDomain, BlogPostResponseDTO>()
                .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count))
                .ReverseMap();

            CreateMap<BlogPostRequestDTO, BlogPostDomain>().ReverseMap();

            CreateMap<CommentDomain, CommentResponseDTO>().ReverseMap();
       
            CreateMap<CommentRequestDTO, CommentDomain>().ReverseMap();
        }
    }
}
