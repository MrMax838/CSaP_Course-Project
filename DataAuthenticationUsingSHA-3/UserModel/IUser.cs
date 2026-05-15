using CSaP.CourseProject.RSA.Key;

namespace CSaP.CourseProject.DataModel
{
    public interface IUser
    {
        public string UserID { get; }

        public byte[] Sign(byte[] data);
        public bool Verify(byte[] data, byte[] signature);

        public RSAData Export();
    }
}