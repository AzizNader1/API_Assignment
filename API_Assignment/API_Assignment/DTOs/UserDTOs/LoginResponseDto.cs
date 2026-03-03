namespace API_Assignment.DTOs.UserDTOs
{
    public class LoginResponseDto
    {
        public string Message { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = [];
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresOn { get; set; }
    }
}
