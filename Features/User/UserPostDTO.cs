using System.Text.Json.Serialization;

namespace Habits.Features.User
{
    public class UserPostDTO
    {
        [JsonPropertyName("username")]
        public required string Username { get; set; }
        [JsonPropertyName("email")]
        public required string Email { get; set; }
        [JsonPropertyName("password")]
        public required string Password { get; set; }
        [JsonPropertyName("minGoal")]
        public required decimal MinGoal { get; set; }
    }

    public class UserPostValidationDTO
    {
        [JsonPropertyName("email")]
        public required string Email { get; set; }
    }
}
