using System;

namespace POEPART2
{
    internal class ActivityLogEntry     //class to store individual log entries for the activity tracker, with a timestamp and description
    {
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"[{Timestamp:dd MMM yyyy HH:mm}] {Description}";
        }
    }
}