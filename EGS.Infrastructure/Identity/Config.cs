using Duende.IdentityServer.Models;

namespace EGS.Infrastructure.Identity
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("egsbooks", "EGS Book Store")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,                    
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },                    
                    AllowedScopes = { "egsbooks" }
                }
            };
    }
}
