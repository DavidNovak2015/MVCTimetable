using System;
using System.ComponentModel.DataAnnotations;
using CLTimeTableDB;

namespace MVCTimetable.Models
{
    public class AdminFeedbackDeleteViewModel
    {
        DbRepository dbRepository = new DbRepository();

        [Display(Name ="Identifizierungsnummer")]
        [Required(ErrorMessage = "Geben Sie bitte eine Identifizierungsnummer an")]
        public int? Id { get; set; }

        public FeedbackEntity MessageToDelete { get; private set; }

        private int GetIdMessage(int? idMessage)
        {
            if (!idMessage.HasValue)
                throw new InvalidOperationException($"{nameof(idMessage)} is empty");

            return idMessage.Value;
        }

        public bool FindMessage(int? id)
        {
            int idMessage = GetIdMessage(id);
            bool found;
            FeedbackEntityDL messageFromDB = dbRepository.GetMessage(idMessage);
            if (messageFromDB == null)
            {
                return found = false;
            }
            MessageToDelete = new FeedbackEntity(messageFromDB.Id,
                                                 messageFromDB.RequestType,
                                                 messageFromDB.Message,
                                                 messageFromDB.Email,
                                                 messageFromDB.PhoneNumber 
                                                );
            return found = true;
        }

        public string DeleteMessage(int? id)
        {
            int idMessage = GetIdMessage(id);
            return dbRepository.DeleteMessage(idMessage);
        }
    }
}