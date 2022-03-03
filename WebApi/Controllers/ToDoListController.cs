using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DAL;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoListController : ControllerBase
    {
        private BlazorDbContext dbContext;

        public ToDoListController(BlazorDbContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IEnumerable<Schedule> GetList()
        {
            return dbContext.Schedules.Include(s => s.Items).OrderByDescending(s => s.Date);
        }

        [Route("{date:datetime}")]
        [HttpGet]
        public List<ToDoItem> GetToDoList(DateTime date)
        {
            return dbContext.Schedules.Include(s => s.Items)
                .FirstOrDefault(s => s.Date.Date == date.Date)?.Items
                .OrderBy(i => i.Index).ToList();
        }

        [HttpPost]
        public int AddSchedule([FromBody]DateTime date)
        {
            var schedule = dbContext.Schedules.FirstOrDefault(s => s.Date.Date == date.Date);
            if (schedule != null)
            {
                return schedule.Id;
            }

            schedule = new Schedule { Date = date.Date, Items = new List<ToDoItem>() };
            dbContext.Schedules.Add(schedule);
            dbContext.SaveChanges();
            return schedule.Id;
        }

        [Route("{date:datetime}")]
        [HttpPost]
        public int? AddToDoItem([FromBody]string content, DateTime date)
        {
            var schedule = dbContext.Schedules.Include(s => s.Items)
                .FirstOrDefault(s => s.Date.Date == date.Date);
            if (schedule == null)
            {
                schedule = new Schedule { Date = date.Date, Items = new List<ToDoItem>() };
                dbContext.Schedules.Add(schedule);
            }

            var newItem = new ToDoItem { Index = schedule.Items.Count + 1, Content = content };
            schedule.Items.Add(newItem);
            dbContext.SaveChanges();
            return newItem.Id;
        }

        [HttpPut]
        public ToDoItem UpdateToDoItem([FromBody]ToDoItem toDoItem)
        {
            var item = dbContext.ToDoItem.FirstOrDefault(i => i.Id == toDoItem.Id);
            if (item == null)
            {
                return null;
            }

            item.Index = toDoItem.Index;
            item.Content = toDoItem.Content;
            item.Status = toDoItem.Status;
            dbContext.ToDoItem.Update(item);
            dbContext.SaveChanges();
            return item;
        }

        // [Route("{id:int}")]
        // [HttpDelete]
        // public void DeleteToDoItem(int id)
        // {
        //     var item = dbContext.ToDoItem.FirstOrDefault(i => i.Id == id);
        //     if (item == null)
        //     {
        //         return;
        //     }
        //
        //     dbContext.ToDoItem.Remove(item);
        //     dbContext.SaveChanges();
        // }
        //
        // [Route("ev/{name}")]
        // [HttpGet]
        // public string GetEnvironmentValue(string name)
        // {
        //     return Environment.GetEnvironmentVariable(name);
        // }
    }
}