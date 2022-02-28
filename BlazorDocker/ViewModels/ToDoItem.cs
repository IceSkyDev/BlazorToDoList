namespace BlazorDocker.ViewModels
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
        public string Background => Status == 0 ? "white" : "#ddd";
    }
}