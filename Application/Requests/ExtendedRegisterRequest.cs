using Microsoft.AspNetCore.Identity.Data;

namespace MoviesAPI.Application.Requests
{
    public class ExtendedRegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public RegisterRequest RegisterRequest { get; set; }
    }
}
