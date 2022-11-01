using System.Runtime.Serialization;

namespace Dragon.Core.Common
{
    public class UserFriendlyException:Exception
    {
        public string? ErrorCode { get; set; }

        public object[] Parameters { get; set; }

        public UserFriendlyException()
        {
        }

        public UserFriendlyException(string message)
            : base(message)
        {
        }

        public UserFriendlyException(string errorCode, params object[] parameters)
            : this(null, errorCode, parameters)
        {
        }

        public UserFriendlyException(Exception? innerException, string errorCode, params object[] parameters)
            : base(null, innerException)
        {
            ErrorCode = errorCode;
            Parameters = parameters;
        }

        public UserFriendlyException(string message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public UserFriendlyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}
