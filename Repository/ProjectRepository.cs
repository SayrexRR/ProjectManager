using Microsoft.EntityFrameworkCore;
using ProjectManager.DataLayer.Context;
using ProjectManager.DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.DataLayer.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext db;

        public ProjectRepository(DataContext context)
        {
            db = context;
        }

        public List<Project> GetProjects()
        {
            return db.Projects.ToList();
        }

        public void AddProject(Project project)
        {
            db.Projects.Add(project);
            db.SaveChanges();
        }

        public void UpdateProject(Project project)
        {
            var updateProject = db.Projects.Find(project.Id);

            updateProject.Name = project.Name;
            updateProject.Description = project.Description;
            updateProject.StartDate = project.StartDate;
            updateProject.EndDate = project.EndDate;
            
            db.Projects.Update(updateProject);
            db.SaveChanges();
        }

        public void DeleteProject(Guid id) 
        {
            var project = db.Projects.Find(id);
            
            db.Projects.Remove(project);
            db.SaveChanges();
        }

        public Project FindProjectById(Guid id)
        {
            return db.Projects.Find(id);
        }

        public List<MyTask> GetProjectTasks(Guid projectId)
        {
            return db.Tasks.Where(t => t.ProjectId == projectId).ToList();
        }

        public MyTask GetTaskById(Guid id)
        {
            return db.Tasks.Find(id);
        }

        public void AddTask(MyTask task)
        {
            db.Tasks.Add(task);
            db.SaveChanges();
        }

        public void UpdateTask(MyTask task)
        {
            var updateTask = db.Tasks.Include(t => t.Points).FirstOrDefault(t => t.Id == task.Id);

            updateTask.Title = task.Title;
            updateTask.ProjectId = task.ProjectId;
            updateTask.Description = task.Description;
            updateTask.Status = task.Status;
            updateTask.CreatedAt = task.CreatedAt;
            updateTask.Deadline = task.Deadline;

            db.Tasks.Update(updateTask);
            db.SaveChanges();
        }

        public bool TaskExists(Guid id)
        {
            return db.Tasks.Any(t => t.Id == id);
        }

        public MyTask GetFirstTask(Guid id)
        {
            return db.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public void DeleteTask(Guid id)
        {
            var task = db.Tasks.Find(id);

            db.Tasks.Remove(task);
            db.SaveChanges();
        }

        public List<Point> GetTaskPoints(Guid taskId)
        {
            return db.Points.Where(p => p.TaskId == taskId).ToList();
        }

        public void AddPoint(Point point)
        {
            db.Points.Add(point);
            db.SaveChanges();
        }

        public void UpdatePoint(Point point)
        {
            db.Points.Update(point);
            db.SaveChanges();
        }
    }
}
