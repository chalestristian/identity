using System.ComponentModel.DataAnnotations;

namespace Identity.Application.DTOs.Request;

public class UserRegistrationRequestDTO
{
    [Required(ErrorMessage = "Field {0} is required")]
    [EmailAddress(ErrorMessage = "Field {0} is not valid")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Field {0} is required")]
    [StringLength(50, ErrorMessage = "Field {0} must be between {2} and {1} characters", MinimumLength = 8)]
    public string Password { get; set; }
    
    [Compare(nameof(Password), ErrorMessage = "Password fields must match")]
    public string PasswordConfirmation { get; set; }
}