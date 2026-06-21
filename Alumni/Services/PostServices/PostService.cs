using Alumni.DTOS;
using Alumni.DTOS.Common;
using Alumni.Models.Core;
using Alumni.Models.Feeds;
using Alumni.Repository.PostRepository;
using Alumni.Services.PostServices;
using AutoMapper;

namespace Alumni.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IPostRepo _postRepo;
        private readonly IMapper _mapper;

        public PostService(IPostRepo postRepo, IMapper mapper)
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<PostResponseDTO>> CreatePostService(CreatePostDTO createPostDto, Guid userId, UserRole userRole)
        {
            var post = _mapper.Map<Post>(createPostDto);
            post.AuthorId = userId;

            var createdPost = await _postRepo.CreatePostRepository(post);
            var responseDto = _mapper.Map<PostResponseDTO>(createdPost);

            return ServiceResponse<PostResponseDTO>.Success(responseDto, "Post created successfully!");
        }

        public async Task<ServiceResponse<IEnumerable<PostResponseDTO>>> GetAllPostService()
        {
            var posts = await _postRepo.GetAllPostRepository();

            var postResponseList = _mapper.Map<IEnumerable<PostResponseDTO>>(posts);

            return ServiceResponse<IEnumerable<PostResponseDTO>>.Success(postResponseList, "Posts retrieved successfully!");
        }
    }
}