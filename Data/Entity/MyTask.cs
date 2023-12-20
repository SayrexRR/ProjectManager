using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.DataLayer.Entity
{
    public class MyTask
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Deadline {  get; set; }
        public Status? Status { get; set; }

        public virtual Project? Project{ get; set; }
        public virtual ICollection<Point>? Points { get; set; }
    }

    public enum Status
    {
        InProcess,
        Completed,
        Expired,
        Postponed
    }
}
