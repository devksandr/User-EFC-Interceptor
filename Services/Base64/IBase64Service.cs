namespace User_EFC_Interceptor.Services.Base64
{
    public interface IBase64Service
    {
        public string Encode(string text);
        public string Decode(string bytes);
    }
}
