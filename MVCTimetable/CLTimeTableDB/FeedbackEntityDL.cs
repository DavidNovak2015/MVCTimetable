namespace CLTimeTableDB
{
    public class FeedbackEntityDL
    {
        public int Id { get; private set; }
        public string RequestType { get; private set; }
        public string Message { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        public FeedbackEntityDL(string email,string phoneNumber,string message,string requestType)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            Message = message;
            RequestType = requestType;
        }
        public FeedbackEntityDL(int id,string requestType,string message,string email,string phoneNumber)
        {
            Id = id;
            RequestType = requestType;
            Message = message;
            Email = email;
            PhoneNumber = phoneNumber;
        }
        public FeedbackEntityDL()
        { }
        public FeedbackEntityDL(int iD)
        {
            Id = iD;
        }
    }
}
