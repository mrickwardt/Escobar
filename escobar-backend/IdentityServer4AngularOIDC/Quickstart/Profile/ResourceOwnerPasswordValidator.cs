using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Validation;
using Server.Models;

namespace IdentityServer4.Quickstart.UI.Profile
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator 
    {
        private readonly UserContext _userContext;

        public ResourceOwnerPasswordValidator(UserContext userContext)
        {
            _userContext = userContext;
        }


        //this is used to validate your user account with provided grant at /connect/token
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                //get your user model from db (by username - in my case its email)
                var user = _userContext.Users.FirstOrDefault(us => us.Email == context.UserName);
                if (user == null)
                {
                    context.Result = new GrantValidationResult
                    {
                        IsError = true,
                        Error = "Usuário ou senha Incorretos"
                    };
                    return Task.CompletedTask;
                }
                
//                var userPasswordHashed = PasswordManager.PasswordManager.PasswordToHashBase64(context.Password, user.PasswordSalt);
                var userPasswordHashed = user.Senha;
                
                if (user.Senha != userPasswordHashed) {
                    context.Result = new GrantValidationResult
                    {
                        IsError = true,
                        Error = "Usuário ou senha Incorretos"
                    };
                    return Task.CompletedTask;
                } 

                context.Result = new GrantValidationResult(user.ID.ToString(), OidcConstants.AuthenticationMethods.Password);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                //TODO Add log.
                context.Result = new GrantValidationResult
                {
                    IsError = true,
                    Error = "Erro desconhecido"
                };
            }
            return Task.CompletedTask;
        }
    }
}