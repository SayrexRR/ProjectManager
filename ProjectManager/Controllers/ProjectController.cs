using Microsoft.AspNetCore.Mvc;
using ProjectManager.BusinessLayer.Models;
using ProjectManager.BusinessLayer.Service;

namespace ProjectManager.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService service;

        public ProjectController(IProjectService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            var projects = service.GetProjects();

            return View(projects);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProjectModel project)
        {
            project.StartDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                service.AddProject(project);

                return RedirectToAction("Index");
            }

            return View(project);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = service.FindProjectById(id.Value);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, ProjectModel project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                service.UpdateProject(project);

                return RedirectToAction(nameof(Index));
            }

            return View(project);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = service.FindProjectById(id.Value);

            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            service.DeleteProject(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
