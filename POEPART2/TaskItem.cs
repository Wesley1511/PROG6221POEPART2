using System;

namespace POEPART2
{
    internal class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; } // nullable, since reminder is optional
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}