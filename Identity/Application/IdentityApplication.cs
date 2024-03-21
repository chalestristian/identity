using Identity.Application.DTOs.Request;
using Identity.Application.DTOs.Response;
using Identity.Application.Interfaces;
using Identity.Domain.Interfaces;

namespace Identity.Application;

public class IdentityApplication : IIdentityApplication
{ 
    private readonly IIdentityService _identityService;

    public IdentityApplication(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    
    
    
    public async Task<UserRegistrationResponseDTO> CadastrarUsuario(UserRegistrationRequestDTO usuarioCadastro)
        => await _identityService.CadastrarUsuario(usuarioCadastro);

    public async Task<UserLoginResponseDTO> Login(UserLoginRequestDTO usuarioLogin)
        => await _identityService.Login(usuarioLogin);

    public async Task<UserLoginResponseDTO> LoginSemSenha(string usuarioId)
        => await _identityService.LoginSemSenha(usuarioId);
}