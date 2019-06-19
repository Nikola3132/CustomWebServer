using SULS.Data;
using SULS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SULS.Services
{
    public class SubmissionService : ISumbissionService
    {
        private readonly SULSContext dbContext;

        public SubmissionService(SULSContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool CreateSubmission(string code,string problemId,string userId)
        {
            Problem problem = this.dbContext.Problems.SingleOrDefault(p => p.Id == problemId);
            User user = this.dbContext.Users.SingleOrDefault(u => u.Id == userId);

            int problemTotalPoints = problem.Points;

            int achievedResult = new Random().Next(0, problemTotalPoints);

            Submission submissionForDb = new Submission()
            {
                AchievedResult = achievedResult,
                Code = code,
                CreatedOn = DateTime.UtcNow,
                Problem = problem,
                User = user
            };

            this.dbContext.Submissions.Add(submissionForDb);
            this.dbContext.SaveChanges();

            return true;
        }

        public Problem GetCurrentProblem(string problemId)
        {
            return this.dbContext.Problems.SingleOrDefault(p => p.Id == problemId);
        }
    }
}
