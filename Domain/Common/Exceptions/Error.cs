using System.Text.Json.Serialization;


namespace Domain.Common.Exceptions
{
    public class Error
    {
        [JsonConstructor]
        public Error(ErrorCodeEnum errorCode, string? customMessage = null)
        {
            ErrorCode = Convert.ToInt32(errorCode);
            Message = customMessage ?? errorCode.ToStringValue();
        }

        public int ErrorCode { get; set; }

        public string Message { get; set; }

        private string? _logMessage;

        public string LoggerMessage
        {
            get { return string.IsNullOrEmpty(_logMessage) ? string.Format("Error Code: {0}, Error Message: {1}", ErrorCode, Message) : _logMessage; }
            set { _logMessage = value; }
        }
    }
}
