using Identity.Application.DTOs.Request;
using Identity.Application.DTOs.Response;

namespace Identity.Domain.Interfaces;

public interface IIdentityService
{
    Task<UserRegistrationResponseDTO> CadastrarUsuario(UserRegistrationRequestDTO usuarioCadastro);
    Task<UserLoginResponseDTO> Login(UserLoginRequestDTO usuarioLogin);
    Task<UserLoginResponseDTO> LoginSemSenha(string usuarioId); 
}