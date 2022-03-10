using System;
using System.Threading.Tasks;

namespace CCLLC.CDS.RestConnector
{
    public interface IAADTokenGenerator
    {
        Task<IAccessToken> GetTokenAsync(Uri resource, string aadTenentId, string clientId, string clientSecret);
    }
}
