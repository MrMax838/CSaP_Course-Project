using CSaP.CourseProject.DataModel;

namespace CSaP.CourseProject
{
    public sealed class ApplicationContext
    {
        private UserRepository _users;
        private MessageRepository _messages;


        public UserRepository Users { get { return _users; } }
        public MessageRepository Messages { get { return _messages; } }


        public ApplicationContext(UserRepository users, MessageRepository messages)
        {
            _users = users;
            _messages = messages;
        }
    }
}