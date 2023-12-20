﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BusinessLayer.Models
{
    public class PointModel
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}
