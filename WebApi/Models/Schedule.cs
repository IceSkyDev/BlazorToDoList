using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<ToDoItem> Items { get; set; }
    }
}