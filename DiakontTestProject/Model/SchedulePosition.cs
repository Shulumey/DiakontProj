using System;

namespace DiakontTestProject.Model
{
    public class SchedulePosition
    {
        public ReferenceItem Department { get; set; }

        public ReferenceItem Position { get; set; }

        public DateTime ApplyFromDate { get; set; }

        public int EmployersCount { get; set; }

        public SchedulePosition(ReferenceItem department, ReferenceItem position, DateTime applyFrom, int employersCount)
        {
            Department = department;
            Position = position;
            ApplyFromDate = applyFrom;
            EmployersCount = employersCount;
        }
    }
}
