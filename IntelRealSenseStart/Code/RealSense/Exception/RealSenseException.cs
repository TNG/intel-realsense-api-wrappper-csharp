using System;
using System.Runtime.Serialization;

namespace IntelRealSenseStart.Code.RealSense.Exception
{
    [Serializable]
    public class RealSenseException : System.Exception
    {
        public RealSenseException(String message) : base(message)
        {
        }

        public RealSenseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}