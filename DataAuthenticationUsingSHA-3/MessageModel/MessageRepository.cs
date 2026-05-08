using System.Text.Json;

namespace CSaP.CourseProject.DataModel
{
    public class MessageRepository
    {
        private string _path = @"D:\University\3 курс\КСтП +КП\Курсова робота\Code\DataAuthenticationUsingSHA-3\IOData\Messages.json";


        public MessageRepository() {}
        
        public MessageRepository(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException(nameof(path));

            _path = path;

            if (!File.Exists(_path))
            {
                File.WriteAllText(_path, "[]");
            }
        }


        private SignedMessage ToMessage(MessageRecord record)
        {
            return new SignedMessage(record.MessageID,
                record.SenderID,
                record.Message,
                Convert.FromBase64String(record.Signature),
                record.Timestamp);
        }

        private MessageRecord ToRecord(SignedMessage message)
        {
            return new MessageRecord() 
            { 
                MessageID = message.MessageID, 
                SenderID = message.SenderID, 
                Message = message.Message, 
                Signature = Convert.ToBase64String(message.Signature), 
                Timestamp = message.Timestamp
            };
        }

        public bool Exists(string messageID)
        {
            return ReadAll().Any(m => m.MessageID == messageID);
        }

        public SignedMessage Get(string messageID)
        {
            MessageRecord record = ReadAll().First(m => m.MessageID == messageID);

            return ToMessage(record);
        }

        public IEnumerable<SignedMessage> GetAll()
        {
            return ReadAll().Select(ToMessage);
        }

        public void Add(SignedMessage message)
        {
            List<MessageRecord> records = ReadAll();

            if (records.Any(x => x.MessageID == message.MessageID))
            {
                throw new InvalidOperationException("Message already exists");
            }

            records.Add(ToRecord(message));

            SaveAll(records);
        }

        public void Delete(string messageID)
        {
            List<MessageRecord> records = ReadAll();

            records.RemoveAll(x => x.MessageID == messageID);

            SaveAll(records);
        }

        private List<MessageRecord> ReadAll()
        {
            string json = File.ReadAllText(_path);

            List<MessageRecord>? records = JsonSerializer.Deserialize<List<MessageRecord>>(json);

            return records ?? new();
        }

        private void SaveAll(List<MessageRecord> records)
        {
            string json = JsonSerializer.Serialize(records, new JsonSerializerOptions() { WriteIndented = true });

            File.WriteAllText(_path, json);
        }
    }
}