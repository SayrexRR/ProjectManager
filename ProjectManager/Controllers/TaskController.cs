using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManager.DataLayer.Context;
using ProjectManager.DataLayer.Entity;

namespace ProjectManager.Controllers
{
    [Route("Project/{projectId}/Task")]
    public class TaskController : Controller
    {
        private readonly DataContext context;

        public TaskController(DataContext context)
        {
            this.context = context;
        }

        // GET: Projects/1/Tasks
        public async Task<IActionResult> Index(Guid projectId)
        {
            var project = await context.Projects.FindAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }

            var tasks = await context.Tasks.Where(t => t.ProjectId == projectId).ToListAsync();
            ViewBag.Project = project;

            return View(tasks);
        }

        // GET: Projects/1/Tasks/Create
        [Route("Create")]
        public async Task<IActionResult> Create(Guid projectId)
        {
            var project = await context.Projects.FindAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }

            ViewBag.Project = project;

            return View();
        }

        // POST: Projects/1/Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(Guid projectId, MyTask task)
        {
            task.CreatedAt = DateTime.Now;

            if (ModelState.IsValid)
            {
                task.ProjectId = projectId;
                context.Add(task);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { projectId = projectId });
            }

            var project = await context.Projects.FindAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }

            ViewBag.Project = project;

            return View(task);
        }

        // GET: Projects/1/Tasks/Edit/5
        [Route("Edit/{id?}")]
        public async Task<IActionResult> Edit(Guid? id, Guid projectId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            if (task.ProjectId != projectId)
            {
                return BadRequest();
            }

            var project = await context.Projects.FindAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }

            ViewBag.Project = project;
            return View(task);
        }

        // POST: Projects/1/Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id?}")]
        public async Task<IActionResult> Edit(Guid id, Guid projectId, MyTask task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(task);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
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

            var project = await context.Projects.FindAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }

            ViewBag.Project = project;
            return View(task);
        }

        // GET: Projects/1/Tasks/Delete/5
        [Route("Delete/{id?}")]
        public async Task<IActionResult> Delete(Guid? id, Guid projectId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            if (task.ProjectId != projectId)
            {
                return BadRequest();
            }

            var project = await context.Projects.FindAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }

            ViewBag.Project = project;
            return View(task);
        }

        // POST: Projects/1/Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id, Guid projectId)
        {
            var task = await context.Tasks.FindAsync(id);
            context.Tasks.Remove(task);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { projectId = projectId });
        }

        private bool TaskExists(Guid id)
        {
            return context.Tasks.Any(e => e.Id == id);
        }
    }
}
