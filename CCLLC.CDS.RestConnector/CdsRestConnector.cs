namespace CCLLC.CDS.RestConnector
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using CCLLC.Core.Serialization;

    public class CdsRestConnector : ICdsRestConnector
    {
        private IJSONContractSerializer Serializer { get; } 

        public CdsRestConnector(IJSONContractSerializer serializer)
        {
            this.Serializer = serializer;
        }

        public async Task<HttpResponseMessage> PostAsync(Uri requestUri, IAccessToken accessToken, string body = null)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken.Value);

            var msg = new HttpRequestMessage(HttpMethod.Post, requestUri);
            msg.Headers.Add("OData-MaxVersion", "4.0");
            msg.Headers.Add("OData-Version", "4.0");

            if (body != null)
            {
                msg.Content = new StringContent(body, UnicodeEncoding.UTF8, "application/json");
            }

            var result = await client.SendAsync(msg);
            return result;
        }

        public async Task<HttpResponseMessage> PostAsync(Uri requestUri, IAccessToken accessToken, ISerializableData data)
        {
            if (data is null)
            {
                return await PostAsync(requestUri, accessToken);
            }

            var body = data.ToString(Serializer);
            return await PostAsync(requestUri, accessToken, body);
        }

        public async Task<TResult> PostAsync<T,TResult>(Uri requestUri, IAccessToken accessToken, T data) where T : ISerializableData where TResult : class, ISerializableData
        {
            var result = await PostAsync(requestUri, accessToken, data);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(string.Format("{0} {1}",(int)result.StatusCode,result.ReasonPhrase));
            }

            var content = await result.Content.ReadAsStringAsync();
            return Serializer.Deserialize<TResult>(content);
        }
    }
}
