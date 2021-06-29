using System;
using System.Runtime.Serialization;

namespace TimeSheetApproval.Domain.Exceptions
{
    [Serializable]
    public class AdAccountInvalidException : Exception
    {
        public AdAccountInvalidException(string adAccount, Exception ex)
            : base($"AD Account \"{adAccount}\" is invalid.", ex)
        {
        }
        protected AdAccountInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
