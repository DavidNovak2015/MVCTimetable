using System.ComponentModel.DataAnnotations;

namespace MVCTimetable.Models
{
    public class FeedbackEntity
    {
        [Display(Name = "Identifizierungsnummer")]
        public int Id { get; private set; }

        [Display(Name = "Anforderungstyp")]
        public string RequestType { get; private set; }

        [Display(Name = "Bericht")]
        [DataType(DataType.MultilineText)]
        public string Message { get; private set; }

        [Display(Name ="Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; private set; }

        [Display(Name = "Handynummer")]
        public string PhoneNumber { get; private set; }

        public FeedbackEntity(int id,string requestType,string message,string email,string phoneNumber)
        {
            Id = id;
            RequestType = requestType;
            Message = message;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}