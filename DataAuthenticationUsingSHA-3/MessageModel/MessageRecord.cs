namespace CSaP.CourseProject.DataModel
{
    public sealed class MessageRecord
    {
        public string MessageID { get; set; } = string.Empty;
        public string SenderID { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Signature { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}