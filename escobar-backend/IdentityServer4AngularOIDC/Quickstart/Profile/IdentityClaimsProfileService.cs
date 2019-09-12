using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Server.Models;

namespace IdentityServer4.Quickstart.UI.Profile
{
    public class IdentityClaimsProfileService : IProfileService
    {
        private readonly UserContext _userContext;

        public IdentityClaimsProfileService(UserContext userContext)
        {
            _userContext = userContext;
        }

        //Get user profile date in terms of claims when calling /connect/userinfo
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");

                if (!string.IsNullOrEmpty(userId?.Value) )
                {
                    //get user from db (find user by user id)
                    var user = _userContext.Users.FirstOrDefault(us => us.ID == Guid.Parse(userId.Value));

                    // issue the claims for the user
                    if (user != null)
                    {
                        context.IssuedClaims = context.Subject.Claims.ToList();
                    }
                }
            }
            catch (Exception)
            {
                //log your error
            }
            return Task.CompletedTask;
        }

        //check if user account is active.
        public Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                //get subject from context (set in ResourceOwnerPasswordValidator.ValidateAsync),
                var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "user_id");

                if (!string.IsNullOrEmpty(userId?.Value) && long.Parse(userId.Value) > 0)
                {
                    var user = _userContext.Users.FirstOrDefault(us => us.ID == Guid.Parse(userId.Value));
                }
            }
            catch (Exception)
            {
                //handle error logging
            }
            return Task.CompletedTask;
        }
    }
    
}