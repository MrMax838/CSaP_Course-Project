namespace CSaP.CourseProject.Service
{
    public sealed class VerificationResult
    {
        private bool _isValid;
        private string _messageID;
        private string _senderID;
        private byte[] _messagehash;
        private byte[] _signature;
        private DateTime _timestamp;


        public bool IsValid { get { return _isValid; } }
        public string MessageID { get { return _messageID; } }
        public string SenderID { get { return _senderID; } }
        public byte[] MessageHash { get { return _messagehash; } }
        public byte[] Signature { get { return _signature; } }
        public DateTime Timestamp { get { return _timestamp; } }


        public VerificationResult(bool isValid, string messageID, string senderID, byte[] messageHash, byte[] signature, DateTime timestamp)
        {
            _isValid = isValid;
            _messageID = messageID;
            _senderID = senderID;
            _messagehash = messageHash;
            _signature = signature;
            _timestamp = timestamp;
        }
    }
}