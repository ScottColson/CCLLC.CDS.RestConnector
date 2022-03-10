namespace CCLLC.CDS.RestConnector
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.IdentityModel.Clients.ActiveDirectory;

    public class AADTokenGenerator : IAADTokenGenerator
    {    
        public AADTokenGenerator() { }       

        public async Task<IAccessToken> GetTokenAsync(Uri resource, string aadTenentId, string clientId, string clientSecret)
        {            
            ClientCredential credentials = new ClientCredential(clientId, clientSecret);

            var authority = string.Format("https://login.microsoftonline.com/{0}", aadTenentId);
            var authContext = new AuthenticationContext(authority);

            var result = await authContext.AcquireTokenAsync(resource.AbsoluteUri, credentials);

            return new AccessToken(result.AccessToken);
        }                   
    }
}
