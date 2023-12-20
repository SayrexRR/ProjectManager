using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManager.BusinessLayer.Models;
using ProjectManager.BusinessLayer.Service;

namespace ProjectManager.Controllers
{
    [Route("Project/{projectId}/Task")]
    public class TaskController : Controller
    {
        private readonly IProjectService service;

        public TaskController(IProjectService service)
        {
            this.service = service;
        }

        public IActionResult Index(Guid projectId)
        {
            var project = service.FindProjectById(projectId);
            if (project == null)
            {
                return NotFound();
            }

            var tasks = service.GetProjectTasks(projectId);
            ViewBag.Project = project;

            return View(tasks);
        }

        [Route("Create")]
        public IActionResult Create(Guid projectId)
        {
            var project = service.FindProjectById(projectId);
            if (project == null)
            {
                return NotFound();
            }

            ViewBag.Project = project;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public IActionResult Create(Guid projectId, TaskModel task)
        {
            task.CreatedAt = DateTime.Now;
            task.Status = Status.InProcess;
            if (ModelState.IsValid)
            {
                task.ProjectId = projectId;
                service.AddTask(task);
                return RedirectToAction(nameof(Index), new { projectId = projectId });
            }

            var project = service.FindProjectById(projectId);
            if (project == null)
            {
                return NotFound();
            }

            ViewBag.Project = project;

            return View(task);
        }

        [Route("Edit/{id?}")]
        public IActionResult Edit(Guid? id, Guid projectId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = service.GetTaskById(id.Value);
            if (task == null)
            {
                return NotFound();
            }
            if (task.ProjectId != projectId)
            {
                return BadRequest();
            }

            var project = service.FindProjectById(projectId);
            if (project == null)
            {
                return NotFound();
            }

            ViewBag.Project = project;
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id?}")]
        public IActionResult Edit(Guid id, Guid projectId, TaskModel task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.UpdateTask(task);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!service.TaskExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index), new { projectId = projectId });
            }

            var project = service.FindProjectById(projectId);
            if (project == null)
            {
                return NotFound();
            }

            ViewBag.Project = project;
            return View(task);
        }

        [Route("Delete/{id?}")]
        public IActionResult Delete(Guid? id, Guid projectId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = service.GetFirstTask(id.Value);
            if (task == null)
            {
                return NotFound();
            }
            if (task.ProjectId != projectId)
            {
                return BadRequest();
            }

            var project = service.FindProjectById(projectId);
            if (project == null)
            {
                return NotFound();
            }

            ViewBag.Project = project;
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id?}")]
        public IActionResult DeleteConfirmed(Guid id, Guid projectId)
        {
            service.DeleteTask(id);
            return RedirectToAction(nameof(Index), new { projectId = projectId });
        }
    }
}
