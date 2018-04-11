using System.Collections.Generic;
using System.Linq;
using CLTimeTableDB;

namespace MVCTimetable.Models
{
    public class AdminFeedbackDisplayViewModel
    {
        DbRepository dbRepository = new DbRepository();
        public List<FeedbackEntity> DisplayMessages { get; private set; }

        public AdminFeedbackDisplayViewModel()
        {
            DisplayMessages = new List<FeedbackEntity>();

            List<FeedbackEntityDL> messagesFromDB = dbRepository.GetMessages();
            DisplayMessages = messagesFromDB.Select(x => new FeedbackEntity(x.Id,
                                                                            x.RequestType,
                                                                            x.Message,
                                                                            x.Email,
                                                                            x.PhoneNumber
                                                               )).ToList();
        }
    }
}