using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POEPART2
{
    internal class ActivityLogger
    {
        private List<ActivityLogEntry> entries = new List<ActivityLogEntry>();

        public void Log(string description)
        {
            entries.Add(new ActivityLogEntry
            {
                Timestamp = DateTime.Now,
                Description = description
            });
        }

        public string GetLogAsText()
        {
            if (entries.Count == 0)
                return "No actions have been logged yet.";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Activity Log:");

            // most recent first
            foreach (var entry in entries.OrderByDescending(e => e.Timestamp))
            {
                sb.AppendLine(entry.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}