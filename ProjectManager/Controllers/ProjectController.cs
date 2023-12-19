using Microsoft.AspNetCore.Mvc;
using ProjectManager.DataLayer.Context;
using ProjectManager.DataLayer.Entity;

namespace ProjectManager.Controllers
{
    public class ProjectController : Controller
    {
        private readonly DataContext dataContext;

        public ProjectController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IActionResult Index()
        {
            var projects = dataContext.Projects.ToList();

            return View(projects);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                dataContext.Projects.Add(project);
                dataContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(project);
        }
    }
}
