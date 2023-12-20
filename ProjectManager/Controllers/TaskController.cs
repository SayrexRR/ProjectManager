using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

        public IActionResult Index(Guid projectId, string searchString)
        {
            ViewBag.Search = searchString;


            var project = service.FindProjectById(projectId);
            if (project == null)
            {
                return NotFound();
            }

            var tasks = service.GetProjectTasks(projectId);

            if (!string.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(t => t.Title.Contains(searchString)).ToList();
            }

            foreach (var task in tasks)
            {
                if (!(task.Status == Status.Completed))
                {
                    if (DateTime.Now > task.Deadline)
                    {
                        task.Status = Status.Expired;
                    }
                }
            }
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
                service.UpdateTask(task);

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

        [Route("Complete/{id?}")]
        public IActionResult Complete(Guid? id, Guid projectId)
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

        [HttpPost, ActionName("Complete")]
        [ValidateAntiForgeryToken]
        [Route("Complete/{id?}")]
        public IActionResult CompleteConfirmed(Guid id, Guid projectId)
        {
            var task = service.GetTaskById(id);

            task.Status = Status.Completed;

            service.UpdateTask(task);

            return RedirectToAction(nameof(Index), new { projectId = projectId });
        }
    }
}
