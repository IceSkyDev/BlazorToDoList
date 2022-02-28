using System;
using System.Collections.Generic;

namespace BlazorDocker.ViewModels
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<ToDoItem> Items { get; set; }
    }
}