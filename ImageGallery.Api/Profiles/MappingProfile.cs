using AutoMapper;
using ImageGallery.Api.Models;
using ImageGallery.Api.Models.Dtos;

namespace ImageGallery.Api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<UserImage, UserImageDto>().ReverseMap();
        }
    }
}
