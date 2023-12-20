using ProjectManager.BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BusinessLayer.Service
{
    public interface IProjectService
    {
        List<ProjectModel> GetProjects();
        void AddProject(ProjectModel project);
        void UpdateProject(ProjectModel project);
        void DeleteProject(Guid id);
        ProjectModel FindProjectById(Guid id);
        List<TaskModel> GetProjectTasks(Guid projectId);
        TaskModel GetTaskById(Guid id);
        void AddTask(TaskModel task);
        void UpdateTask(TaskModel task);
        bool TaskExists(Guid id);
        TaskModel GetFirstTask(Guid id);
        void DeleteTask(Guid id);
    }
}
