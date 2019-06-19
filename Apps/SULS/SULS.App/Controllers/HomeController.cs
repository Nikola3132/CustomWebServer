using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Problems;
using SULS.Services;
using System.Collections.Generic;
using System.Linq;

namespace SULS.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProblemService problemService;

        public HomeController(IProblemService problemService)
        {
            this.problemService = problemService;
        }

        [HttpGet(Url = "/")]
        public IActionResult IndexSlash()
        {
            return this.Index();
        }

        public IActionResult Index()
        {
            List<ProblemHomeViewModel> homeProblems = new List<ProblemHomeViewModel>();

            if (this.IsLoggedIn())
            {
                homeProblems = this.problemService.GetAllProblems().Select(p => new ProblemHomeViewModel
                {
                    Id = p.Id,
                    Count = this.problemService.GetAllSubmissionsOfTheProblem(p.Id),
                    Name = p.Name
                }).ToList();
            }

            return this.View(homeProblems);
        }
    }
}