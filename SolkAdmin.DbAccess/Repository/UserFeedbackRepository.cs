using SolkAdmin.DTO;
using SolkAdmin.DTO.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolkAdmin.DbAccess.Repository
{
    public class UserFeedbackRepository : BaseRepository
    {
        public UserFeedbackRepository(AppDBContext context) : base(context)
        {
        }

        public long Add(IUserFeedback Feedback)
        {
            UserFeedback feedback = new UserFeedback
            {
                UserId = Feedback.UserId,
                Description = Feedback.Description,
                Date = Feedback.Date,
                Rating = Feedback.Rating
            };
            context.UserFeedbacks.Add(feedback);
            context.SaveChanges();
            return feedback.Id;
        }
    }
}
