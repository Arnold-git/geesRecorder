﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorder.Shared.DTOs
{
    public class MarkAttendanceDTO
    {
        public int FingerprintId { get; set; }

        public int EventId { get; set; }

        public DateTime AttendanceTimeStamp { get; set; }
    }
}
