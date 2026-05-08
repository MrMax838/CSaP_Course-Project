using CSaP.CourseProject.DataModel;

namespace CSaP.CourseProject
{
    public sealed class ApplicationContext
    {
        private int _rsaKeySiza;
        private UserRepository _users;
        private MessageRepository _messages;


        public int RsaKeySiza { get { return _rsaKeySiza; } }
        public UserRepository Users { get { return _users; } }
        public MessageRepository Messages { get { return _messages; } }


        public ApplicationContext(UserRepository users, MessageRepository messages, int rsaKeySiza = 1024)
        {
            _rsaKeySiza = rsaKeySiza;
            _users = users;
            _messages = messages;
        }
    }
}