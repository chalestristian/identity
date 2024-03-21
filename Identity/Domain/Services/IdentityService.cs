using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Identity.Application.DTOs.Request;
using Identity.Application.DTOs.Response;
using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using Identity.Domain.Interfaces.Repository;
using Identity.Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Identity.Domain.Services;

public class IdentityService : IIdentityService
{
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserRepositoryAsync _userRepository;
        private readonly JwtOptions _jwtOptions;
        private readonly IUnitOfWork _unitOfWork;

        public IdentityService(SignInManager<IdentityUser> signInManager,
                               UserManager<IdentityUser> userManager,
                               IOptions<JwtOptions> jwtOptions,
                               IUserRepositoryAsync userRepository,
                               IUnitOfWork unitOfWork
                               )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserRegistrationResponseDTO> CadastrarUsuario(UserRegistrationRequestDTO usuarioCadastro)
        {
            var identityUser = new IdentityUser
            {
                UserName = usuarioCadastro.Email,
                Email = usuarioCadastro.Email,
                EmailConfirmed = true,
                LockoutEnd = DateTimeOffset.Now
            };

            var result = await _userManager.CreateAsync(identityUser, usuarioCadastro.Password);
            if (result.Succeeded)
                await _userManager.SetLockoutEnabledAsync(identityUser, false);

            Random random = new Random();
            int numeroAleatorio = random.Next(1, 1001);
            
            var user = new User();
            user.Email = "THALIIIIIS";
            user.Id = numeroAleatorio;
            
            var cancellationToken = new CancellationToken();
            await _userRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            
            var usuarioCadastroResponse = new UserRegistrationResponseDTO(result.Succeeded);
            if (!result.Succeeded && result.Errors.Count() > 0)
                usuarioCadastroResponse.AddErrors(result.Errors.Select(r => r.Description));

            return new UserRegistrationResponseDTO();
        }

        public async Task<UserLoginResponseDTO> Login(UserLoginRequestDTO usuarioLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Password, false, true);
            if (result.Succeeded)
                return await GerarCredenciais(usuarioLogin.Email);

            var usuarioLoginResponse = new UserLoginResponseDTO();
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    usuarioLoginResponse.AddError("Essa conta está bloqueada");
                else if (result.IsNotAllowed)
                    usuarioLoginResponse.AddError("Essa conta não tem permissão para fazer login");
                else if (result.RequiresTwoFactor)
                    usuarioLoginResponse.AddError("É necessário confirmar o login no seu segundo fator de autenticação");
                else
                    usuarioLoginResponse.AddError("Usuário ou senha estão incorretos");
            }

            return usuarioLoginResponse;
        }

        public async Task<UserLoginResponseDTO> LoginSemSenha(string usuarioId)
        {
            var usuarioLoginResponse = new UserLoginResponseDTO();
            var usuario = await _userManager.FindByIdAsync(usuarioId);
            
            if (await _userManager.IsLockedOutAsync(usuario))
                usuarioLoginResponse.AddError("Essa conta está bloqueada");
            else if (!await _userManager.IsEmailConfirmedAsync(usuario))
                usuarioLoginResponse.AddError("Essa conta precisa confirmar seu e-mail antes de realizar o login");
            
            if (usuarioLoginResponse.Success)
                return await GerarCredenciais(usuario.Email);

            return usuarioLoginResponse;
        }

        private async Task<UserLoginResponseDTO> GerarCredenciais(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var accessTokenClaims = await ObterClaims(user, adicionarClaimsUsuario: true);
            var refreshTokenClaims = await ObterClaims(user, adicionarClaimsUsuario: false);

            var dataExpiracaoAccessToken = DateTime.Now.AddSeconds(_jwtOptions.AccessTokenExpiration);
            var dataExpiracaoRefreshToken = DateTime.Now.AddSeconds(_jwtOptions.RefreshTokenExpiration);

            var accessToken = GerarToken(accessTokenClaims, dataExpiracaoAccessToken);
            var refreshToken = GerarToken(refreshTokenClaims, dataExpiracaoRefreshToken);

            return new UserLoginResponseDTO
            (
                success: true,
                accessToken: accessToken,
                refreshToken: refreshToken
            );
        }

        private string GerarToken(IEnumerable<Claim> claims, DateTime dataExpiracao)
        {
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: dataExpiracao,
                signingCredentials: _jwtOptions.SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private async Task<IList<Claim>> ObterClaims(IdentityUser user, bool adicionarClaimsUsuario)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

            if (adicionarClaimsUsuario)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                claims.AddRange(userClaims);

                foreach (var role in roles)
                    claims.Add(new Claim("role", role));
            }

            return claims;
        }
    }
