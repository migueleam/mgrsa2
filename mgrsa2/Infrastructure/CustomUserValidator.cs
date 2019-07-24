using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using mgrsa2.Models;

namespace mgrsa2.Infrastructure {

    public class CustomUserValidator : UserValidator<AppUser> {

        public override async Task<IdentityResult> ValidateAsync(
                UserManager<AppUser> manager,
                AppUser user) {

            IdentityResult result = await base.ValidateAsync(manager, user);

            List<IdentityError> errors = result.Succeeded ?
                new List<IdentityError>() : result.Errors.ToList();

            if (!user.Email.ToLower().EndsWith("@elaviso.com")) {
                errors.Add(new IdentityError {
                    Code = "EmailDomainError",
                    Description = "Only example.com email addresses are allowed"
                });
            }

            return errors.Count == 0 ? IdentityResult.Success
                : IdentityResult.Failed(errors.ToArray());
        }
    }
}
