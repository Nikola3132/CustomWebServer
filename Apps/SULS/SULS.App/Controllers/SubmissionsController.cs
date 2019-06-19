using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Problems;
using SULS.Models;
using SULS.Services;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SULS.App.BindingModels;

namespace SULS.App.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ISumbissionService sumbissionService;
        public SubmissionsController(ISumbissionService sumbissionService)
        {
            this.sumbissionService = sumbissionService;
            
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {

            var problemId = this.Request.QueryData["id"].FirstOrDefault();
            Problem currentProblem = this.sumbissionService.GetCurrentProblem(problemId);

            return this.View(new ProblemSubmissionViewModel
            {
                Name = currentProblem.Name,
                ProblemId = problemId
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(SubmissionBindingModel submissionBindingModel)
        {
            var problemId = this.Request.FormData["ProblemId"].FirstOrDefault();
            var userId = this.User.Id;

            this.sumbissionService.CreateSubmission(submissionBindingModel.Code, problemId,userId);

            return this.Redirect("/");
        }
    }
}
