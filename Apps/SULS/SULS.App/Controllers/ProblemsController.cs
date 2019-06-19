using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SULS.App.BindingModels;
using SULS.App.ViewModels.Problems;
using SULS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SULS.App.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemService problemService;

        public ProblemsController(IProblemService problemService)
        {
            this.problemService = problemService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(ProblemBindingModel problemBindingModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }
            this.problemService
                .CreateProblem(problemBindingModel.Name, problemBindingModel.Points);

            return this.Redirect("/");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details()
        {
            var problemId = this.Request.QueryData["id"].FirstOrDefault();

            var currentProblem = this.problemService.GetProblemById(problemId);

            var problemSubmissions = this.problemService.ProblemSubmissions(problemId)
                .Select(p=>new ProblemDetailsViewModel
                {
                    AchievedResult =(p.AchievedResult).ToString(),
                    CreatedOn = p.CreatedOn.ToString("dd/MM/yyyy"),
                    MaxPoints =(p.AchievedResult / currentProblem.Points).ToString(),
                    Username = this.User.Username
                });

           


            return this.View(problemSubmissions);
        }
    }
}
