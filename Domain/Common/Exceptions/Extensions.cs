namespace Domain.Common.Exceptions
{
    public static class Extensions
    {
        /// <summary>
        ///     https://cisart.wordpress.com/2013/09/11/string-value-attribute-for-enums/
        ///     Example Enum to String:
        ///     TestEnum test = TestEnum.Value2;
        ///     string str = test.ToStringValue();
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToStringValue(this Enum value)
        {
            if (!string.IsNullOrEmpty(value.ToString())) {
                var type = value.GetType();
                if (type != null && type.IsEnum)
                {
                    var field = type.GetField(value.ToString() ?? "");
                    if (field != null)
                    {
                        var attributes = (StringValueAttribute[])field.GetCustomAttributes(typeof(StringValueAttribute), false);
                        return ((attributes.Length > 0)) ? attributes[0].Value : value.ToString();
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        ///     Get exception message, return inner exception if exists
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetExceptionMessage(this Exception ex)
        {
            return ex.InnerException != null ? GetExceptionMessage(ex.InnerException) : ex.Message;
        }
    }
}
