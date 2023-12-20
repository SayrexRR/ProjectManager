using Microsoft.AspNetCore.Mvc;
using ProjectManager.BusinessLayer.Models;
using ProjectManager.BusinessLayer.Service;

namespace ProjectManager.Controllers
{
    [Route("Task/{taskId}/Point")]
    public class PointController : Controller
    {
        private readonly IProjectService service;

        public PointController(IProjectService service)
        {
            this.service = service;
        }

        public IActionResult Index(Guid taskId)
        {
            var task = service.GetTaskById(taskId);
            if (task == null)
            {
                return NotFound();
            }

            var points = service.GetTaskPoints(taskId);

            ViewBag.Points = points;
            ViewBag.Task = task;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Guid taskId, PointModel point) 
        {
            var task = service.GetTaskById(taskId);
            if (task == null)
            {
                return NotFound();
            }

            point.IsCompleted = false;
            if (ModelState.IsValid)
            {
                point.TaskId = taskId;
                service.AddPoint(point);
                return View();
            }

            ViewBag.Task = task;

            return View(point);
        }

        //public IActionResult GoBack(Guid taskId)
        //{

        //}
    }
}
