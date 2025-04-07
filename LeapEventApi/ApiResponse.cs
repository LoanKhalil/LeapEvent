namespace LeapEventApi
{
    public class ApiResponse<T>
    {
        public ApiResponse(T data, bool appError = false, string message = "")
        {
            Data = data;
            AppError = appError;
            Message = message;
        }

        public T Data { get; }
        public bool AppError { get; }
        public string Message { get; }
        
    }
}
