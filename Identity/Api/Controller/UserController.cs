using Identity.Application.Interfaces;
using System.Net;
using System.Security.Claims;
using Identity.Application.DTOs.Request;
using Identity.Application.DTOs.Response;
using Identity.Web.Api.Controller.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Web.Api.Controller;

[ApiVersion("1.0")]
[ApiController]
[Route("api/[controller]/[action]")]

public class UserController : ApiControllerBase
{
    private IIdentityApplication _identityApplication;

    public UserController(IIdentityApplication identityApplication) =>
        _identityApplication = identityApplication;

 
    [ProducesResponseType(typeof(UserRegistrationResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost("cadastro")]
    public async Task<IActionResult> Cadastrar(UserRegistrationRequestDTO usuarioCadastro)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var resultado = await _identityApplication.CadastrarUsuario(usuarioCadastro);
        if (resultado.Success)
            return Ok(resultado);
        else if (resultado.Errors.Count > 0)
        {
            var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: resultado.Errors);
            return BadRequest(problemDetails);
        }
        
        return StatusCode(StatusCodes.Status500InternalServerError);
    }

  
    [ProducesResponseType(typeof(UserRegistrationResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost("login")]
    public async Task<ActionResult<UserRegistrationResponseDTO>> Login(UserLoginRequestDTO usuarioLogin)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var resultado = await _identityApplication.Login(usuarioLogin);
        if (resultado.Success)
            return Ok(resultado);

        return Unauthorized();
    }

    
    [ProducesResponseType(typeof(UserRegistrationResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [Authorize]
    [HttpPost("refresh-login")]
    public async Task<ActionResult<UserRegistrationResponseDTO>> RefreshLogin()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var usuarioId = identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (usuarioId == null)
            return BadRequest();

        var resultado = await _identityApplication.LoginSemSenha(usuarioId);
        if (resultado.Success)
            return Ok(resultado);
        
        return Unauthorized();
    }
}