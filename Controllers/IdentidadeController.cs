using IdentidadeAmandaStore.Core.Email.Config;
using IdentidadeAmandaStore.Core.Token;
using IdentidadeAmandaStore.Domain.Contexto;
using IdentidadeAmandaStore.Extensions;
using IdentidadeAmandaStore.Models;
using IdentidadeAmandaStore.Services;
using IdentidadeAmandaStore.Services.ValidacaoIdentity.LoginValidate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore.Controllers
{
    [ApiController]
    [Route("api/Identidade")]
    public class IdentidadeController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRegistrarUsuario _registrarUsuario;
        private readonly AppSettings _appSettings;
        private readonly IdentidadeAmandaStoreContext _DbIdentity;
        private readonly IValidateLogin _validateLogin;
        public IdentidadeController(SignInManager<IdentityUser> signInManager,
                                    UserManager<IdentityUser> userManager,
                                    IRegistrarUsuario registrarUsuario,
                                    IOptions<AppSettings> appSettings,
                                    IdentidadeAmandaStoreContext storeContext,
                                    IValidateLogin validateLogin) 
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _registrarUsuario = registrarUsuario;
            _appSettings = appSettings.Value;
            _DbIdentity = storeContext;
            _validateLogin = validateLogin;
        }

        [HttpPost("Nova-Identidade")]
        public async Task<IActionResult> NovaIdentidade(UsuarioRegistro usuario)
        {
            try
            {
                var result = await _registrarUsuario.RegistrarIdentidade(usuario);

                if (result.Status == 1) return BadRequest(result);
                return Ok(result);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message+" "+ex.InnerException);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UsuarioLogin userLogin)
        {
            try
            {
                var result = await _validateLogin.Validar(userLogin);

                if (result.Status == 1) return BadRequest(result);

                await GerarJwt(userLogin.Login);

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message + " " + ex.InnerException);
            }       
        }

        [HttpPost("esqueceu-senha")]
        public async Task<IActionResult> EsqueciASenha(UsuarioEsqueciSenhaViewModel usuarioVm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _userManager.FindByEmailAsync(usuarioVm.Email);

            if (user == null)
                return BadRequest("Email não encontrado");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var disparadorEmail = new DispararEmail(new EmailRecuperarSenha(token, usuarioVm.Email));
            await disparadorEmail.Disparar();

            return Ok("Token de recuperação enviado ao email.");
        }
        private async Task<UsuarioRespostaLogin> GerarJwt(string login)
        {
            var user = await _userManager.FindByNameAsync(login);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await ObterClaimsUsuario(claims, user);
            var encodedToken = CodificarToken(identityClaims);

            return ObterRespostaToken(encodedToken, user, claims);
        }
        private async Task<ClaimsIdentity> ObterClaimsUsuario(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private UsuarioRespostaLogin ObterRespostaToken(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
        {
            return new UsuarioRespostaLogin
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UsuarioToken = new UsuarioToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        private string CodificarToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

    }
}
