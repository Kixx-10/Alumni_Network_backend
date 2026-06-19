namespace Alumni.DTOS.Common
{
    public class ServiceResponse<T>
    {
        public bool IsSuccess { get; set; } = true;
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? ErrorCode { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow.Date;

        public static ServiceResponse<T> Success(T data, string message)
        {
            return new ServiceResponse<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message,
                ErrorCode = null

            };
        }
        public static ServiceResponse<T> Failure(string errorCode, string message)
        {
            return new ServiceResponse<T>
            {
                IsSuccess = false,
                Data = default,
                Message = message,
                ErrorCode = errorCode
            };
        }

    }

}
