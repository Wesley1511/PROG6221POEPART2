using System;

namespace POEPART2
{
    internal class ActivityLogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"[{Timestamp:dd MMM yyyy HH:mm}] {Description}";
        }
    }
}