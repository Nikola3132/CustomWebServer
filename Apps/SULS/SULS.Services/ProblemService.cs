using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SULS.Data;
using SULS.Models;

namespace SULS.Services
{
    public class ProblemService : IProblemService
    {
        private readonly SULSContext dbContext;

        public ProblemService(SULSContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool CreateProblem(string name, int totalPoints)
        {
            Problem problemForDb = new Problem()
            {
                Name = name,
                Points = totalPoints
            };

            this.dbContext.Problems.Add(problemForDb);
            this.dbContext.SaveChanges();

            return true;
        }

        public bool DeleteSubmission(string submissionId)
        {
            //TODO:
            return true;
        }

        public IList<Problem> GetAllProblems()
        {
            return this.dbContext.Problems.ToList();
        }

        public int GetAllSubmissionsOfTheProblem(string problemId)
        {
            int problemSubmissions 
                = this.dbContext.Submissions.Include(s => s.Problem)
                .Where(s => s.ProblemId == problemId)
                .Count();

            return problemSubmissions;
        }

        public Problem GetProblemById(string problemId)
        {
            return this.dbContext.Problems.FirstOrDefault(p => p.Id == problemId);
        }

        public IList<Submission> ProblemSubmissions(string problemId)
        {
            return this.dbContext
                .Submissions
                .Include(s => s.Problem)
                .Where(p => p.ProblemId == problemId)
                .ToList();
        }
    }
}
