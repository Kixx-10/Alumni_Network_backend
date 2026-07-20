using Alumni.Models.Feeds;
using Alumni.Models.Master;
using System.ComponentModel.DataAnnotations;

namespace Alumni.Models.Core
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; }
        public bool IsOnline { get; set; } = false;
        public DateTime? LastSeen { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.Date;

        public DateTime? UpdatedDate { get; set; }

        //navigation properties 
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Share> Shares { get; set; } = new List<Share>();
        public virtual Profile? Profile { get; set; }

        public virtual ICollection<FriendRequest> SentFriendRequests { get; set; } = new List<FriendRequest>();
        public virtual ICollection<FriendRequest> ReceivedFriendRequests { get; set; } = new List<FriendRequest>();
        public virtual ICollection<Conversation> ConversationsAsUser1 { get; set; }
            = new List<Conversation>();

        public virtual ICollection<Conversation> ConversationsAsUser2 { get; set; }
            = new List<Conversation>();

        public virtual ICollection<Message> SentMessages { get; set; }
            = new List<Message>();

        public virtual ICollection<Message> ReceivedMessages { get; set; }
            = new List<Message>();

    }
}
