using ProjectManager.DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.DataLayer.Repository
{
    public interface IProjectRepository
    {
        List<Project> GetProjects();
        void AddProject(Project project);
        void UpdateProject(Project project);
        void DeleteProject(Guid id);
        Project FindProjectById(Guid id);
        List<MyTask> GetProjectTasks(Guid projectId);
        MyTask GetTaskById(Guid id);
        void AddTask(MyTask task);
        void UpdateTask(MyTask task);
        bool TaskExists(Guid id);
        MyTask GetFirstTask(Guid id);
        void DeleteTask(Guid id);
        List<Point> GetTaskPoints(Guid taskId);
        void AddPoint(Point point);
        void UpdatePoint(Point point);
    }
}
