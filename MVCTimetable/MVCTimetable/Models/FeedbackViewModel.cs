using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using CLTimeTableDB;

namespace MVCTimetable.Models
{
    public class FeedbackViewModel
    {
        DbRepository DbRepository = new DbRepository();
        public List<SelectListItem> RequestTypes { get; private set; }

        [Display(Name ="Ihr Email:")]
        [Required(ErrorMessage ="Geben Sie bitte Ihr Email an")]
        public string Email { get; set; }

        [Display(Name ="Ihre Handynummer")]
        public string PhoneNumber { get; set; }

        [Display(Name ="Bericht")]
        [MaxLength(1000,ErrorMessage ="Ihre Bericht ist zu lang. Maximum ist tausend Zeichen")]
        public string Message { get; set; }

        [Display(Name = "Anforderungstyp")]
        public string RequestType { get; set; }
        
        public FeedbackViewModel()
        {
            RequestTypes = new List<SelectListItem>();
            
            RequestTypes.Add(new SelectListItem { Text = "Anforderungstyp", Value = "RequestTyp" });
            RequestTypes.Add(new SelectListItem { Text = "Belobung", Value = "Compliment" });
            RequestTypes.Add(new SelectListItem { Text = "Beschwerde", Value = "Complaint" });
            RequestTypes.Add(new SelectListItem { Text = "Rufen Sie mich zurück", Value = "CallMeBack" });
            RequestTypes.Add(new SelectListItem { Text = "Sonstige", Value = "Another" });
        }

        public string AddMessageToDatabase(FeedbackViewModel feedbackViewModel)
        {
            FeedbackEntityDL messageToDB = new FeedbackEntityDL(feedbackViewModel.Email, feedbackViewModel.PhoneNumber, feedbackViewModel.Message, feedbackViewModel.RequestType);

            return DbRepository.AddMessageFromWebSite(messageToDB);
        }
    }
}