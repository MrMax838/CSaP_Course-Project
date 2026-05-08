using System.Numerics;
using System.Text.Json;
using CSaP.CourseProject.RSA;

namespace CSaP.CourseProject.DataModel
{
    public class UserRepository
    {
        private string _path = @"D:\University\3 курс\КСтП +КП\Курсова робота\Code\DataAuthenticationUsingSHA-3\IOData\Users.json";


        public UserRepository() {}

        public UserRepository(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException(nameof(path));

            _path = path;

            if (!File.Exists(_path))
            {
                File.WriteAllText(_path, "[]");
            }
        }


        private User ToUser(UserRecord record)
        {
            RSAData data = new RSAData
            {
                PublicExponent = BigInteger.Parse(record.PublicExponent),
                PrivateExponent = BigInteger.Parse(record.PrivateExponent),
                Module = BigInteger.Parse(record.Module)
            };

            RSA.RSA rsa = RSA.RSA.Import(data);

            return new User(record.UserID, rsa);
        }

        private UserRecord ToRecord(User user)
        {
            RSAData data = user.Export();

            return new UserRecord
            {
                UserID = user.UserID,
                PublicExponent = data.PublicExponent.ToString(),
                PrivateExponent = data.PrivateExponent.ToString(),
                Module = data.Module.ToString()
            };
        }

        public bool Exists(string userID)
        {
            return ReadAll().Any(u => u.UserID == userID);
        }

        public User Get(string userID)
        {
            UserRecord record = ReadAll().First(u => u.UserID == userID);

            User user = ToUser(record);

            return user;
        }

        public IEnumerable<IUser> GetAll()
        {
            return ReadAll().Select(ToUser);
        }

        public void Add(User user)
        {
            UserRecord record = ToRecord(user);

            List<UserRecord> users = ReadAll();

            if (users.Any(u => u.UserID == user.UserID))
            {
                throw new InvalidOperationException("User already exists");
            }

            users.Add(record);

            SaveAll(users);
        }

        public void Delete(string userID)
        {
            List<UserRecord> records = ReadAll();

            records.RemoveAll(x => x.UserID == userID);

            SaveAll(records);
        }

        private List<UserRecord> ReadAll()
        {
            string json = File.ReadAllText(_path);

            List<UserRecord>? users = JsonSerializer.Deserialize<List<UserRecord>>(json);

            return users ?? new List<UserRecord>();
        }

        private void SaveAll(List<UserRecord> users)
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions() { WriteIndented = true });

            File.WriteAllText(_path, json);
        }
    }
}