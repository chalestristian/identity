using System.ComponentModel.DataAnnotations;

namespace Identity.Application.DTOs.Request;

public class UserLoginRequestDTO
{
    [Required(ErrorMessage = "Field {0} is required")]
    [EmailAddress(ErrorMessage = "Field {0} is not valid")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Field {0} is required")]
    public string Password { get; set; }
}