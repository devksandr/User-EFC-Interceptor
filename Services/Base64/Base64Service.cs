namespace User_EFC_Interceptor.Services.Base64
{
    public class Base64Service : IBase64Service
    {
        public string Encode(string text)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(bytes);
        }

        public string Decode(string bytes)
        {
            var text = System.Convert.FromBase64String(bytes);
            return System.Text.Encoding.UTF8.GetString(text);
        }
    }
}
