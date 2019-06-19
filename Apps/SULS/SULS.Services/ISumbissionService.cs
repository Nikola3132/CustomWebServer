using SULS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.Services
{
    public interface ISumbissionService
    {
        Problem GetCurrentProblem(string problemId);
        bool CreateSubmission(string code, string problemId,string userId);
    }
}
