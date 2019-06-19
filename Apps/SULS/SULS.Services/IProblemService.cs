using SULS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.Services
{
    public interface IProblemService
    {

        IList<Submission> ProblemSubmissions(string problemId);
        bool CreateProblem(string name,int totalPoints);

        IList<Problem> GetAllProblems();

        int GetAllSubmissionsOfTheProblem(string problemId);

        bool DeleteSubmission(string submissionId);

        Problem GetProblemById(string problemId);

    }
}
