namespace Domain.Common.Exceptions
{
    public class HahnApiException : Exception
    {
        public IList<Error> Errors { get; set; }

        public string LoggerMessage
        {
            get
            {
                var errorMessages = Errors.Select(x => x.LoggerMessage).ToList();
                var descriptions = errorMessages.Any() ? string.Join("; ", errorMessages) : string.Empty;
                return descriptions;
            }
        }

        /// <summary>
        ///     Send back bad request with custom message
        /// </summary>
        /// <param name="customMessage"></param>
        public HahnApiException(string customMessage) : base(customMessage)
        {
            Errors = new List<Error>()
            {
                new Error(ErrorCodeEnum.BadRequest, customMessage)
            };
        }

        /// <summary>
        ///     Send back bad request with custom message and error list
        /// </summary>
        /// <param name="customMessage"></param>
        /// <param name="errors"></param>
        public HahnApiException(string customMessage, List<Error> errors)
            : base(customMessage)
        {
            Errors = errors;
        }

        /// <summary>
        ///     Send back one numerical error code
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="customMessage"></param>
        public HahnApiException(ErrorCodeEnum errorCode, string? customMessage = null)
                : base(string.Format("Error Code: {0}", errorCode))
        {
            Errors = new List<Error>()
            {
                new Error(errorCode, customMessage)
            };
        }

        /// <summary>
        /// Send back one numerical error code + inner exception
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="customMessage"></param>
        /// <param name="inner"></param>
        public HahnApiException(ErrorCodeEnum errorCode, string customMessage, Exception inner)
            : base($"Error Code: {errorCode}", inner)
        {
            Errors = new List<Error>()
            {
                new Error(errorCode, customMessage)
            };
        }

        /// <summary>
        /// Send back multiple numerical error codes + inner exception
        /// </summary>
        /// <param name="errorCodes"></param>
        /// <param name="customMessage"></param>
        /// <param name="inner"></param>
        public HahnApiException(IList<ErrorCodeEnum> errorCodes, string customMessage, System.Exception inner)
            : base($"Error Code: {FormatErrorCodes(errorCodes)}", inner)
        {
            Errors = new List<Error>();
            foreach (var errorCode in errorCodes)
            {
                Errors.Add(new Error(errorCode));
            }
        }

        /// <summary>
        ///     Send back multiple numerical error codes
        /// </summary>
        public HahnApiException(IList<ErrorCodeEnum> errorCodes)
            : base(string.Format("Error Code: {0}", FormatErrorCodes(errorCodes)))
        {
            Errors = new List<Error>();
            foreach (var errorCode in errorCodes)
            {
                Errors.Add(new Error(errorCode));
            }
        }

        private static string FormatErrorCodes(IList<ErrorCodeEnum> errorCodes)
        {
            var errorCodeValues = errorCodes.Select(x => ((int)x).ToString()).ToList();
            var formattedString = errorCodes.Any() ? string.Join("; ", errorCodeValues) : string.Empty;
            return formattedString;
        }
    }
}
