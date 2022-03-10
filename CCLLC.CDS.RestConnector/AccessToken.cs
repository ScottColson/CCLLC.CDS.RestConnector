namespace CCLLC.CDS.RestConnector
{
    public class AccessToken : IAccessToken
    {
        public string Value { get; }

        public AccessToken(string value)
        {
            this.Value = value;
        }
    }
}
