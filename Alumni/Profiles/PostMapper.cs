using Alumni.DTOS;
using Alumni.Models.Feeds;

namespace Alumni.Profiles
{
    public class PostMapper : AutoMapper.Profile
    {
        public PostMapper()
        {
            CreateMap<CreatePostDTO, Post>();
            CreateMap<Post, PostResponseDTO>()
                .ForMember(dest => dest.AuthorName,
                    opt => opt.MapFrom(src => src.Author != null ? src.Author.Name : "Unknown"))
                .ForMember(dest => dest.AuthorAvatarUrl,
                    opt => opt.MapFrom(src =>
                        src.Author != null && src.Author.Profile != null
                            ? src.Author.Profile.AvatarUrl
                            : string.Empty))
                .ForMember(dest => dest.CommentCount,
                    opt => opt.MapFrom(src => src.Comments.Count))
                .ForMember(dest => dest.LikeCount,
                    opt => opt.MapFrom(src => src.Likes.Count))
                .ForMember(dest => dest.ShareCount,
                    opt => opt.MapFrom(src => src.Shares.Count));
        }
    }
}