namespace whats_csharp.Models
{
    public class LoginModel
    {
        public  required string Email { get; set; } = string.Empty;
        public  required string Senha { get; set; } = string.Empty;
    }
}