using CSaP.CourseProject.DataModel;

namespace CSaP.CourseProject.Service
{
    public static class Initializer
    {
        public static ApplicationContext Initialize(
            int rsaKeySiza = 1024, 
            string userPath = "", 
            string messagePath = "", 
            bool cleanInitialization = true)
        {
            string baseDir = GetBaseDir();

            if (userPath == "") userPath = Path.Combine(baseDir, "DataAuthenticationUsingSHA-3", "IOData", "Users.json");
            if (messagePath == "") messagePath = Path.Combine(baseDir, "DataAuthenticationUsingSHA-3", "IOData", "Messages.json");

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


        private static string GetBaseDir()
        {
            string baseDir = AppContext.BaseDirectory;

            while(!File.Exists(Path.Combine(baseDir, "DataAuthenticationUsingSHA-3.sln")))
            {
                baseDir = Directory.GetParent(baseDir)!.FullName;
            }

            return baseDir;
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