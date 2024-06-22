namespace User_EFC_Interceptor.Models
{
    public class ServiceResult<T>
    {
        public T Data { get; }
        public string Message { get; }

        public ServiceResult(T data, string message = "")
        {
            (Data, Message) = (data, message);
        }
    }
}
