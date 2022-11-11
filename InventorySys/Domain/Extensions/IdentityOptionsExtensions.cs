using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extensions
{
    public static class IdentityOptionsExtensions
    {
        /// <summary>
        /// Remove the character type requirements, and use length restrictions only.
        /// </summary>
        /// <param name="options">The Microsoft.AspNetCore.Identity.IdentityOptions instance this method extends</param>
        /// <param name="requiredLength">The minimum length a password must be</param>
        /// <param name="requiredUniqueChars">The minimum number of unique chars a password must comprised of</param>
        /// <returns>The current Microsoft.AspNetCore.Identity.IdentityOptions instance</returns>
        public static IdentityOptions UseLengthOnlyOptions(
            this IdentityOptions options,
            int requiredLength = 8,
            int requiredUniqueChars = 3)
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = requiredLength;
            options.Password.RequiredUniqueChars = requiredUniqueChars;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            options.Lockout.MaxFailedAccessAttempts = 5;
            //options.Lockout.AllowedForNewUsers = true;

            return options;
        }

    }
}
