namespace CSaP.CourseProject.DataModel
{
    public sealed class SignedMessage
    {
        private readonly string _messageID;
        private readonly string _senderID;
        private readonly string _message;
        private readonly byte[] _signature;
        private readonly DateTime _timestamp;


        public string MessageID { get { return _messageID; } }
        public string SenderID { get { return _senderID; } }
        public string Message { get { return _message; } }
        public byte[] Signature { get { return _signature.ToArray(); } }
        public DateTime Timestamp { get { return _timestamp; } }


        public SignedMessage(string messageID, string senderID, string message, byte[] signature, DateTime timestamp)
        {
            if (signature == null) throw new ArgumentNullException(nameof(signature));

            _messageID = messageID;
            _senderID = senderID;
            _message = message;
            _signature = signature.ToArray();
            _timestamp = timestamp;
        }
    }
}