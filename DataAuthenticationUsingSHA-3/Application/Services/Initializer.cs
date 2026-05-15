using CSaP.CourseProject.DataModel;

namespace CSaP.CourseProject.Service
{
    public static class Initializer
    {
        public static ApplicationContext Initialize(
            int rsaKeySiza = 1024, 
            string userPath = @"D:\University\3 курс\КСтП +КП\Курсовий проєкт\Code\DataAuthenticationUsingSHA-3\IOData\Users.json", 
            string messagePath = @"D:\University\3 курс\КСтП +КП\Курсовий проєкт\Code\DataAuthenticationUsingSHA-3\IOData\Messages.json", 
            bool cleanInitialization = true)
        {
            UserRepository users = new();
            MessageRepository messages = new();

            if (cleanInitialization)
            {
                CleanFile(userPath);
                CleanFile(messagePath);

                AddStandartUsers(users, rsaKeySiza);
            }
            else
            {
                EnsureFileInitialized(userPath);
                EnsureFileInitialized(messagePath);
            }

            return new(users, messages);
        }

        private static void EnsureFileInitialized(string path)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "[]");                
            }
        }

        private static void CleanFile(string path)
        {
            File.WriteAllText(path, "[]");
        }

        private static void AddStandartUsers(UserRepository users, int rsaKeySiza)
        {
            string[] userIDs = {"Alice", "Bob", "Eva"};

            foreach (string userID in userIDs)
            {
                users.Add(new User(userID, RSA.RSA.Create(rsaKeySiza)));
            }
        }
    }
}