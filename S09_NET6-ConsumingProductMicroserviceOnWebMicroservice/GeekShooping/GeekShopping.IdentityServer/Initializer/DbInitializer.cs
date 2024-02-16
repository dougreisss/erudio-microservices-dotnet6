using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using IdentityModel;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GeekShopping.IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly MySqlContext _content;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _role;

        public DbInitializer(MySqlContext content, UserManager<ApplicationUser> user, RoleManager<IdentityRole> role)
        {
            _content = content;
            _user = user;
            _role = role;
        }

        public void Initialize()
        {
            if (_role.FindByNameAsync(IdentityConfiguration.Admin).Result != null) { return; }

            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();

            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();

            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "Douglas-Admin",
                Email = "douglas-admin@gmail.com.br",
                EmailConfirmed = true,
                PhoneNumber = "+55 (13) 99155-5052",
                FirstName = "Douglas",
                LastName = "Admin"
            };

            _user.CreateAsync(admin, "Douglas123$").GetAwaiter().GetResult();
            _user.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();

            var adminClaims = _user.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, "Douglas-Admin"),
                new Claim(JwtClaimTypes.GivenName, "Douglas"),
                new Claim(JwtClaimTypes.FamilyName, "Admin"),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
            }).Result;

            ApplicationUser client = new ApplicationUser()
            {
                UserName = "Douglas-Client",
                Email = "douglas-client@gmail.com.br",
                EmailConfirmed = true,
                PhoneNumber = "+55 (13) 99155-5052",
                FirstName = "Douglas",
                LastName = "Client"
            };

            _user.CreateAsync(client, "Douglas123$").GetAwaiter().GetResult();
            _user.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult();

            var clientClaims = _user.AddClaimsAsync(client, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, "Douglas-Client"),
                new Claim(JwtClaimTypes.GivenName, "Douglas"),
                new Claim(JwtClaimTypes.FamilyName, "Client"),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
            }).Result;
        }
    }
}
