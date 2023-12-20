using ProjectManager.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.BusinessLayer.Models;

namespace ProjectManager.BusinessLayer.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository repository;

        public ProjectService(IProjectRepository repository)
        {
            this.repository = repository;
        }

        public List<ProjectModel> GetProjects()
        {
            var projects = repository.GetProjects();

            return projects.Select(p => new ProjectModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                StartDate = p.StartDate,
                EndDate = p.EndDate
            }).ToList();
        }

        public void AddProject(ProjectModel project)
        {
            repository.AddProject(new DataLayer.Entity.Project
            {
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate
            });
        }

        public void UpdateProject(ProjectModel project)
        {
            repository.UpdateProject(new DataLayer.Entity.Project
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate
            });
        }

        public void DeleteProject(Guid id)
        {
            repository.DeleteProject(id);
        }

        public ProjectModel FindProjectById(Guid id)
        {
            var project = repository.FindProjectById(id);

            return new ProjectModel
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate
            };
        }

        public List<TaskModel> GetProjectTasks(Guid projectId)
        {
            var tasks = repository.GetProjectTasks(projectId);

            return tasks.Select(t => new TaskModel
            {
                Id= t.Id,
                ProjectId= t.ProjectId,
                Title = t.Title,
                Description = t.Description,
                CreatedAt = t.CreatedAt,
                Deadline = t.Deadline.Value,
                Status = (Status)t.Status,
                Points = (List<PointModel>)t.Points,
            }).ToList();
        }

        public TaskModel GetTaskById(Guid id)
        {
            var task = repository.GetTaskById(id);

            return new TaskModel
            {
                Id = task.Id,
                Title = task.Title,
                ProjectId = task.ProjectId,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                Deadline = task.Deadline.Value,
                Status = (Status)task.Status,
                Points = (List<PointModel>)task.Points,
            };
        }

        public void AddTask(TaskModel task)
        {
            repository.AddTask(new DataLayer.Entity.MyTask
            {
                Title = task.Title,
                ProjectId = task.ProjectId,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                Deadline = task.Deadline,
                Status = (DataLayer.Entity.Status)task?.Status,
                Points = (List<DataLayer.Entity.Point>)task?.Points
            });
        }

        public void UpdateTask(TaskModel task)
        {
            repository.UpdateTask(new DataLayer.Entity.MyTask
            {
                Id = task.Id,
                Title = task.Title,
                ProjectId = task.ProjectId,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                Deadline = task.Deadline,
                Status = (DataLayer.Entity.Status)task?.Status,
                Points = (List<DataLayer.Entity.Point>)task?.Points
            });
        }

        public bool TaskExists(Guid id)
        {
            return repository.TaskExists(id);
        }

        public TaskModel GetFirstTask(Guid id)
        {
            var task = repository.GetFirstTask(id);

            return new TaskModel
            {
                Id = task.Id,
                Title = task.Title,
                ProjectId = task.ProjectId,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                Deadline = task.Deadline.Value,
                Status = (Status)task.Status,
                Points = (List<PointModel>)task.Points,
            };
        }

        public void DeleteTask(Guid id)
        {
            repository.DeleteTask(id);
        }
    }
}
