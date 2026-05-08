using CSaP.CourseProject.DataModel;

namespace CSaP.CourseProject
{
    public static class Initializer
    {
        public static ApplicationContext Initialize(int rsaKeySiza = 1024, bool createdDefaultUsers = true)
        {
            UserRepository users = new();
            MessageRepository messages = new();

            if (createdDefaultUsers)
            {
                AddUserIfNotExist(users, "Alice", rsaKeySiza);
                AddUserIfNotExist(users, "Bob", rsaKeySiza);
                AddUserIfNotExist(users, "Eva", rsaKeySiza);
            }

            return new(users, messages);
        }

        private static void AddUserIfNotExist(UserRepository users, string userID, int rsaKeySiza)
        {
            if (!users.Exists(userID))
            {
                users.Add(new User(userID, RSA.RSA.Create(rsaKeySiza)));
            }
        }
    }
}