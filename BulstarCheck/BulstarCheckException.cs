
namespace BulstarCheck
{
    using System;

    [Serializable]
    public class BulstarCheckException : ApplicationException
    {
        public string ErrorMessage { get; private set; }
        public string Location { get; private set; }

        private BulstarCheckException()
        {
        }

        public BulstarCheckException(string errorMessage, string location)
        {
            this.ErrorMessage = errorMessage;
            this.Location = location;
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}