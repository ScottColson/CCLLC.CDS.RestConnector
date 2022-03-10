using System;
using System.Net.Http;
using System.Threading.Tasks;
using CCLLC.Core.Serialization;

namespace CCLLC.CDS.RestConnector
{
    public interface ICdsRestConnector
    {
        Task<HttpResponseMessage> PostAsync(Uri requestUri, IAccessToken accessToken, string body = null);

        Task<HttpResponseMessage> PostAsync(Uri requestUri, IAccessToken accessToken, ISerializableData data);

        /// <summary>
        /// Post data to CRM REST endpoint using strongly typed serializable data and results. 
        /// </summary>
        /// <typeparam name="T">Data type of posted data.</typeparam>
        /// <typeparam name="TResult">Data type of resulting data.</typeparam>
        /// <param name="requestUri">The REST endpoint Uri.</param>
        /// <param name="accessToken">Access token for the endpoint.</param>
        /// <param name="data">Strongly typed data of Type <typeparamref name="T"/> that implmenents the <see cref="ISerializableData"/> interface.</param>
        /// <returns>Strongly typed data of Type <typeparamref name="TResult"/> that implements the <see cref="ISerializableData"/> interface.</returns>
        Task<TResult> PostAsync<T,TResult>(Uri requestUri, IAccessToken accessToken, T data) where T : ISerializableData where TResult : class, ISerializableData;
    }
}
