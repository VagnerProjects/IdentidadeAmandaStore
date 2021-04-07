using IdentidadeAmandaStore.Models;
using IdentidadeAmandaStore.Services.Status;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore.Services.ValidacaoIdentity.LoginValidate
{
    public class ValidateLogin: IValidateLogin
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ValidateLogin(UserManager<IdentityUser> userManager,
                            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IServerStatusIdentity> Validar(UsuarioLogin usuarioLogin)
        {
            var validaEmail = await ValidarEmail(usuarioLogin);
            var validaSenha = await ValidarSenha(usuarioLogin);

            if (validaEmail.Status == 1) return await Task.FromResult(validaEmail);

            if (validaSenha.Status == 1) return await Task.FromResult(validaSenha);
           
            return await Task.FromResult(new ServerStatusIdentity() { Mensagem = "Usuário autenticado com sucesso!", Status = 0 });
        }

        private async Task<IServerStatusIdentity> ValidarSenha(UsuarioLogin usuarioLogin)
        {
            var resultPassword = await _signInManager.PasswordSignInAsync(usuarioLogin.Login,
                                                                         usuarioLogin.Senha, false, true);

            if (resultPassword.IsLockedOut)
                return await Task.FromResult(new ServerStatusIdentity() { Mensagem = "Usuario bloqueado por excesso de tentativas", Status = 1 });

            if (!resultPassword.Succeeded)
                return await Task.FromResult(new ServerStatusIdentity() { Mensagem = "Usuario ou senha incorretos!", Status = 1 });

            return await Task.FromResult(new ServerStatusIdentity() { Mensagem = "Senha autenticada!", Status = 0 });
        }

        private async Task<IServerStatusIdentity> ValidarEmail(UsuarioLogin usuarioLogin)
        {
            if (!usuarioLogin.Login.Contains("@"))
                return await Task.FromResult(new ServerStatusIdentity() { Mensagem = "E-mail inválido!", Status = 1 });

            var result = await _userManager.FindByEmailAsync(usuarioLogin.Login);

            if (result == null)
                return await Task.FromResult(new ServerStatusIdentity() { Mensagem = "Usuário não encontrado!", Status = 1 });

            return await Task.FromResult(new ServerStatusIdentity() { Mensagem = "E-mail encontrado", Status = 0 });
        }
    }
}
