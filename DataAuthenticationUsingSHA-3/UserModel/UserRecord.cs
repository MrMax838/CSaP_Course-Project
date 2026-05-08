namespace CSaP.CourseProject.DataModel
{
    public sealed class UserRecord
    {
        public string UserID { get; set; } = string.Empty;
        public string PublicExponent { get; set; } = string.Empty;
        public string PrivateExponent { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
    }
}