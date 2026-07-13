
namespace FastFood.Core.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime Expires { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime Created { get; set; }

        public string UserId { get; set; } = string.Empty;

        public ApplicationUser? User { get; set; }
    }
}