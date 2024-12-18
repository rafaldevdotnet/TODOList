﻿namespace TODOList.Domain.Entities
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public int PercentComplete { get; set; } = 0;
        public bool IsDone { get; set; } = false;
    }
}
