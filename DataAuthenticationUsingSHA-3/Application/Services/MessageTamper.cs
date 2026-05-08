using CSaP.CourseProject.DataModel;

namespace CSaP.CourseProject.Service
{
    public sealed class MessageTamper
    {
        private ApplicationContext _context;


        public MessageTamper(ApplicationContext context)
        {
            _context = context;
        }

        public SignedMessage Tamper(string originalMessageID, string newMessageID, string target, string tamperedMessage)
        {
            SignedMessage original = _context.Messages.Get(originalMessageID);

            return new SignedMessage(newMessageID, target, tamperedMessage, original.Signature, DateTime.UtcNow);
        }
    }
}