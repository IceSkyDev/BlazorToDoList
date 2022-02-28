namespace WebApi.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
    }
}