using Alumni.DTOS;
using Alumni.Models.Feeds;

namespace Alumni.Profiles
{
    public class PostMapper : AutoMapper.Profile
    {
        public PostMapper()
        {
            CreateMap<CreatePostDTO, Post>();

            CreateMap<Post, PostResponseDTO>().ForMember(
                dest => dest.AuthorName,
                opt => opt.MapFrom(src => src.Author != null ? src.Author.Name : "Unknown"));
        }
    }
}