using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorServer.ViewModels;

namespace BlazorServer.Services
{
    public interface IToDoList
    {
        Task<List<Schedule>> GetSchedules();
        Task<List<ToDoItem>> GetToDoItems(DateTime date);
        void AddSchedule(DateTime date);
        void AddToDoItem(DateTime date, string content);
        void UpdateToDoItem(ToDoItem item);
        void DeleteToDoItem(ToDoItem item);
    }

    public class ToDoListService : IToDoList
    {
        
        private readonly HttpClient client;
 
        public ToDoListService(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("BlazorWebApi");
        }
        public async Task<List<Schedule>> GetSchedules()
        {
            var schedules = await client.GetFromJsonAsync<List<Schedule>>($"ToDoList");
            return schedules;
        }

        public async Task<List<ToDoItem>> GetToDoItems(DateTime date)
        {
            var todos = await client.GetFromJsonAsync<List<ToDoItem>>($"ToDoList/{date:yyyy-MM-dd}");
            return todos;
        }

        public async void AddSchedule(DateTime date)
        {
            await client.PostAsJsonAsync($"ToDoList", $"{date:yyyy-MM-dd}");
        }

        public async void AddToDoItem(DateTime date, string content)
        {
            await client.PostAsJsonAsync($"ToDoList/{date:yyyy-MM-dd}", content);
        }

        public async void UpdateToDoItem(ToDoItem item)
        {
            await client.PutAsJsonAsync($"ToDoList", item);
        }

        public async void DeleteToDoItem(ToDoItem item)
        {
            await client.DeleteAsync($"ToDoList/{item.Id}");
        }
    }
}