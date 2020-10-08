﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorder.Models
{
    public class AttendanceEvent
    {        
        public string Name { get; set; }

        public DateTime DateTime { get; set; }

        public DateTime Deadline { get; set; }

        public bool Attended { get; set; }
    }
}