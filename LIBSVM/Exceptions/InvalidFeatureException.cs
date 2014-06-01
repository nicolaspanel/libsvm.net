using System;
using System.Runtime.Serialization;

namespace libsvm
{
    [Serializable]
    public class InvalidFeatureException : Exception
    {
        public InvalidFeatureException()
        {
        }

        public InvalidFeatureException(string message) : base(message)
        {
        }

        public InvalidFeatureException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidFeatureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}